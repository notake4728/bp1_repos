namespace Define
{

    public static class CO
    {
        public const float REEL_WIDTH = 1.28f; //図柄の配置間隔
        public const int DENOMINATOR_PROBABILITY_MAX = 65536; //確率の分母

        // 内容の変更まで制限したいときの配列用 
        public static readonly System.Collections.ObjectModel.ReadOnlyCollection<int> ROLE_RATE_LIST =
        System.Array.AsReadOnly(new int[] { 40, 60, 87, 58, 144, 550, 875, 9460, 8978 });
        
        // 役タイプ
        public enum ROLE_TYPE
        {
            ROLE_TYPE_BIG_01 = 0,   //青7
            ROLE_TYPE_BIG_02,       //赤7
            ROLE_TYPE_BIG_03,       //青白7
            ROLE_TYPE_BIG_04,       //赤白7
            ROLE_TYPE_BIG_05,       //BAR
            ROLE_TYPE_SUICA,        //スイカ
            ROLE_TYPE_CHERRY,       //弱チェリー
            //ROLE_TYPE_CHERRY_02,  //強チェリー
            ROLE_TYPE_BELL,         //ベル
            ROLE_TYPE_REPLAY,       //リプレイ
            //ROLE_TYPE_BLANK,      //ハズレ目
            ROLE_TYPE_MAX,
        }

        // 成立役ビットフラグ
        public enum ROLE_FLG_TYPE
        {
            ROLE_FLG_TYPE_NONE      = 0,
            ROLE_FLG_TYPE_BONUS_01  = 1 << 0,//青7BB
            ROLE_FLG_TYPE_BONUS_02  = 1 << 1,//赤7BB
            ROLE_FLG_TYPE_BONUS_03  = 1 << 2,//青白7BB
            ROLE_FLG_TYPE_BONUS_04  = 1 << 3,//赤白7BB
            ROLE_FLG_TYPE_BONUS_05  = 1 << 4,//BAR
            ROLE_FLG_TYPE_BONUS_MASK = ROLE_FLG_TYPE_BONUS_01 | ROLE_FLG_TYPE_BONUS_02 | ROLE_FLG_TYPE_BONUS_03 | ROLE_FLG_TYPE_BONUS_04 | ROLE_FLG_TYPE_BONUS_05,
            // ここまでボーナス
            ROLE_FLG_TYPE_SUICA     = 1 << 5,//スイカ
            ROLE_FLG_TYPE_CHERRY    = 1 << 6,//チェリー
            ROLE_FLG_TYPE_BELL      = 1 << 7,//ベル
            ROLE_FLG_TYPE_REPLAY    = 1 << 8,//リプレイ
            ROLE_FLG_TYPE_,
        }

        // 図柄タイプ(ここの順番は変えない)
        public enum PATTERN_TYPE
        {
            PATTERN_TYPE_SEVEN_01 = 0,  //赤7
            PATTERN_TYPE_SEVEN_02,      //青7
            PATTERN_TYPE_SEVEN_03,      //白7
            PATTERN_TYPE_BELL,          //ベル
            PATTERN_TYPE_SUICA,         //スイカ
            PATTERN_TYPE_CHERRY,        //チェリー
            PATTERN_TYPE_BAR,           //バー
            PATTERN_TYPE_REPLAY,        //リプレイ
            PATTERN_TYPE_BLANK,         //ブランク
            PATTERN_TYPE_BELL_02,       //ベル2
            PATTERN_TYPE_SUICA_02,      //スイカ2
            PATTERN_TYPE_CHERRY_02,     //チェリー2
            PATTERN_TYPE_REPLAY_02,     //リプレイ2
            PATTERN_TYPE_MAX,
        }

        public enum REEL_TYPE
        {
            REEL_TYPE_LEFT = 0,
            REEL_TYPE_CENTER,
            REEL_TYPE_RIGHT,
            REEL_TYPE_MAX,
        }

        public enum REEL_POS_TYPE
        {
            REEL_POS_TYPE_TOP = 0,
            REEL_POS_TYPE_MIDDLE,
            REEL_POS_TYPE_BOTTOM,
            REEL_POS_TYPE_MAX
        }

        //---------------------------------------------
        // 停止テーブル定義
        //---------------------------------------------
        // 左リール(下段)
        public static readonly System.Collections.ObjectModel.ReadOnlyCollection<int[]> LEFT_REEL_STOP_TABLE_LIST =
        System.Array.AsReadOnly
            (
            new int[][]
            {
                new int[] {0,0,0,0,0,0,0,0,0,0,0,0,4,3,2,1,0,1,0,0}, //青7
                new int[] {0,0,0,0,0,0,0,0,0,0,0,0,4,3,2,1,0,1,0,0}, //赤7
                new int[] {0,0,0,0,0,0,0,0,0,0,0,0,4,3,2,1,0,1,0,0}, //青白7
                new int[] {0,0,0,0,0,0,0,0,0,0,0,0,4,3,2,1,0,1,0,0}, //赤白7
                new int[] {0,0,0,4,3,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0}, //BAR
                new int[] {3,2,1,0,4,3,2,1,0,4,3,2,1,0,4,3,2,1,0,4}, //スイカ
                new int[] {0,0,4,3,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, //チェリー
                new int[] {2,1,0,4,3,2,1,0,4,3,2,1,0,4,3,2,1,0,4,3}, //ベル
                new int[] {2,1,0,4,3,2,1,0,4,3,2,1,0,4,3,2,1,0,4,3}, //リプレイ
                new int[] {0,0,0,4,3,2,1,0,0,0,0,1,0,0,0,0,0,0,0,0}, //役無し
            }
            );

        // 中リール(中段)
        public static readonly System.Collections.ObjectModel.ReadOnlyCollection<int[]> MIDDLE_REEL_STOP_TABLE_LIST =
        System.Array.AsReadOnly
            (
            new int[][]
            {
                new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,4,3,2,1,0,0,0}, //青7
                new int[] {0,0,0,0,0,0,0,0,4,3,2,1,0,0,0,0,0,0,0,0}, //赤7
                new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,4,3,2,1,0,0,0}, //青白7
                new int[] {0,0,0,0,0,0,0,0,4,3,2,1,0,0,0,0,0,0,0,0}, //赤白7
                new int[] {2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,3}, //BAR
                new int[] {3,2,1,0,4,3,2,1,0,4,3,2,1,0,4,3,2,1,0,4}, //スイカ
                new int[] {1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0}, //チェリー
                new int[] {1,0,4,3,2,1,0,4,3,2,1,0,4,3,2,1,0,4,3,2}, //ベル
                new int[] {0,4,3,2,1,0,4,3,2,1,0,4,3,2,1,0,4,3,2,1}, //リプレイ
                new int[] {2,1,0,1,0,2,1,0,1,0,4,3,2,1,0,2,1,0,1,0}, //役無し
            }
            );

        // 右リール(上段)
        public static readonly System.Collections.ObjectModel.ReadOnlyCollection<int[]> RIGHT_REEL_STOP_TABLE_LIST =
        System.Array.AsReadOnly
            (
            new int[][]
            {
                new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,4,3,2,1,0,2}, //青7
                new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,4,3,2,1,0,2}, //赤7
                new int[] {0,0,0,0,4,3,2,1,0,2,1,0,0,0,0,0,0,0,0,0}, //青白7
                new int[] {0,0,0,4,3,2,1,0,3,2,1,0,0,0,0,0,0,0,0,0}, //赤白7
                new int[] {0,0,0,0,0,0,0,0,0,0,4,3,2,1,0,0,0,0,0,0}, //BAR
                new int[] {1,0,4,3,2,1,0,4,3,2,1,0,4,3,2,1,0,4,3,2}, //スイカ
                new int[] {1,0,4,3,2,1,0,4,3,2,1,0,4,3,2,1,0,4,3,2}, //チェリー
                new int[] {2,1,0,4,3,2,1,0,4,3,2,1,0,4,3,2,1,0,4,3}, //ベル
                new int[] {2,1,0,4,3,2,1,0,4,3,2,1,0,4,3,2,1,0,4,3}, //リプレイ
                new int[] {1,0,0,3,2,1,0,1,0,2,1,0,0,3,2,1,0,1,0,2}, //役無し
            }
            );
    }
}