using NLog;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Postgres
{
    public sealed class PostgresConnector
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public static string connectionString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};CommandTimeout=3600;", "127.0.0.1", "5432", "postgres", "zkc4zbx6", "PT4_2015_02_03_211924");

        public static Dictionary<String,String> getDefaultStatForPlayer(String name)
        {
            _logger.Info("Trying ot get stats for player : " + name);
            String dataRequest = @"SELECT (cash_hand_player_statistics.id_player) as ""id_player"",
            (player_real.id_site) as ""id_site"",
            (player.player_name) as ""str_player_name"",
            (sum(CAST((cash_hand_player_statistics.val_curr_conv * cash_hand_player_statistics.amt_won) AS numeric ))) as ""amt_won_curr_conv"",
            (sum((case when(cash_hand_player_statistics.id_hand > 0) then 1 else 0 end))) as ""cnt_hands"",
            (sum((case when(cash_hand_player_statistics.flg_vpip) then 1 else 0 end))) as ""cnt_vpip"",
            (sum((case when(lookup_actions_p.action = '') then 1 else 0 end))) as ""cnt_walks"",
            (sum((case when(cash_hand_player_statistics.cnt_p_raise > 0) then 1 else 0 end))) as ""cnt_pfr"",
            (sum((case when( lookup_actions_p.action LIKE '__%'  OR (lookup_actions_p.action LIKE '_'
                AND (cash_hand_player_statistics.amt_before > (cash_limit.amt_bb + cash_hand_player_statistics.amt_ante))
                AND (cash_hand_player_statistics.amt_p_raise_facing < (cash_hand_player_statistics.amt_before - (cash_hand_player_statistics.amt_blind + cash_hand_player_statistics.amt_ante))) 
                  AND (cash_hand_player_statistics.flg_p_open_opp 
                  OR cash_hand_player_statistics.cnt_p_face_limpers > 0 
                  OR cash_hand_player_statistics.flg_p_3bet_opp 
                  OR cash_hand_player_statistics.flg_p_4bet_opp) )) then 1 
                  else 0 
               end))) as ""cnt_pfr_opp"",
           (sum((case when(cash_hand_player_statistics.flg_p_3bet) then 1 else 0 end))) as ""cnt_p_3bet"",
           (sum((case when(cash_hand_player_statistics.flg_p_3bet_opp) then 1 else 0 end))) as ""cnt_p_3bet_opp"",
           (sum((case when(cash_hand_player_statistics.enum_p_3bet_action='F') then 1 else 0 end))) as ""cnt_p_3bet_def_action_fold"",
           (sum((case when(cash_hand_player_statistics.flg_p_3bet_def_opp) then 1 else 0 end))) as ""cnt_p_3bet_def_opp"" 
        FROM
           cash_hand_player_statistics ,
           player,
           lookup_actions lookup_actions_p,
           cash_limit,
           player player_real 
        WHERE
           (player.id_player = cash_hand_player_statistics.id_player)  
           AND (lookup_actions_p.id_action=cash_hand_player_statistics.id_action_p)  
           AND (cash_limit.id_limit = cash_hand_player_statistics.id_limit)  
           AND (player_real.id_player = cash_hand_player_statistics.id_player_real)   
           AND (
              cash_hand_player_statistics.id_player = (
                 SELECT
                    id_player 
                 FROM
                    player 
                 WHERE
                    player_name_search=E'" + name + @"'  
                    AND id_site='200'
              )
           )       
           AND (
              (cash_hand_player_statistics.id_gametype = 1)
              AND (
                 (cash_hand_player_statistics.id_gametype IN (1))
                 AND (
                    cash_hand_player_statistics.id_limit in (
                       SELECT
                          id_limit 
                       FROM
                          cash_limit 
                       WHERE
                          flg_nl=true
                    )
                 )
              )
           )  
        GROUP BY
           (cash_hand_player_statistics.id_player),
           (player_real.id_site),
           (player.player_name)
        ";
            Dictionary<String, String> dico = new Dictionary<string, string>();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(dataRequest, conn))
                {
                    String[] keys = new String[] { "cnt_hands", "cnt_vpip", "cnt_walks", "cnt_pfr", "cnt_pfr_opp", "cnt_p_3bet", "cnt_p_3bet_opp", "cnt_p_3bet_def_opp", "cnt_p_3bet_def_action_fold" };
                    try
                    {
                        using (NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                foreach (String value in keys)
                                {
                                    dico.Add(value, dr[value].ToString());
                                }
                            }
                            dr.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error occured : " + ex.Message + " \n StackTrace : " + ex.StackTrace);
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            if (dico.Count == 0)
            {
                return null;
            }
            return dico;
        }

        /*
        public List<Tuple<int,String>> getMostWinningPlayer(int number)
        {
            List<Tuple<int, String>> nameList = new List<Tuple<int, String>>();
            _logger.Info("Read the " + number + " most profitable player");
            String dataRequest = "SELECT (cash_hand_player_statistics.id_player) AS \"id_player\", " +
                "(player_real.id_site) AS \"id_site\"," +
                "(player.player_name) AS \"str_player_name\","+
                "(Sum(Cast( (cash_hand_player_statistics.val_curr_conv * cash_hand_player_statistics.amt_won) AS NUMERIC ))) AS \"amt_won_curr_conv\","+
                "(Sum((CASE WHEN( cash_hand_player_statistics.id_hand > 0) THEN 1 ELSE 0 END))) AS \"cnt_hands\"" +
                "FROM cash_hand_player_statistics, player, player player_real " +
                "WHERE (player.id_player = cash_hand_player_statistics.id_player)"+
                "AND (player_real.id_player = cash_hand_player_statistics.id_player_real)" +
                "GROUP BY (cash_hand_player_statistics.id_player), (player_real.id_site), (player.player_name)" +
                "ORDER BY amt_won_curr_conv DESC " + 
                "LIMIT " + number;
            _logger.Info("Run SQL Statement => " + dataRequest);
            NpgsqlCommand command = new NpgsqlCommand(dataRequest, this._conn);
            try
            {
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    nameList.Add(Tuple.Create<int, String>(int.Parse(dr["id_player"].ToString()), dr["str_player_name"].ToString()));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured : " + ex.Message + " \n StackTrace : " + ex.StackTrace);
                throw;
            }
            _logger.Info("End Request for getMostWinningPlayer ! N° object return = " + nameList.Count);
            return nameList;
        }
         * */
    }
}
