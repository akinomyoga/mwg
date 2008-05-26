namespace mwg.Poke{
	public class saveData{
		public byte[] data;
		public saveData(string filename){
			if(!System.IO.File.Exists(filename)){
				System.Windows.Forms.MessageBox.Show("指定したファイルは存在しません\n");
				return;
			}
			//Access 確保
			string pokeSavWfn=filename;
			System.IO.FileStream rbf=new System.IO.FileStream(filename,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			System.IO.BinaryReader rbr=new System.IO.BinaryReader(rbf);
			//--ファイルサイズ取得
			int imax=(int)(new System.IO.FileInfo(filename).Length);//ファイルサイズ取得
			//System.Windows.Forms.MessageBox.Show("ファイルサイズを取得しました - "+imax.ToString()+"\n");
			if(imax!=32768){
				//ファイルサイズによって誤ったファイルを排除
				System.Windows.Forms.MessageBox.Show("正規のファイルではないようです。ご確認下さい。処理を終了します。\n");
				System.Windows.Forms.MessageBox.Show("処理完了\n");
				rbr.Close();
				rbf.Close();
			}
			//--ファイル読込
			this.data=rbr.ReadBytes(imax);
		}
	}

	public class Monster{
		private byte[] data;//長さ44のデータ
		private byte[] name;
		private byte[] pare;
		public Monster(byte[] data,byte[] name,byte[] pare){
			this.data=data;
			this.name=name;
			this.pare=pare;
		}
		public Monster(byte[] data,int DataIndex,int NameIndex,int PareIndex){
			initialize(data,DataIndex,NameIndex,PareIndex);

		}
		public Monster(mwg.Poke.saveData data,int index){
			int iData=DataBase+index*44;
			int iPare=PareBase+index*6;
			int iName=NameBase+index*6;
			initialize(data.data,iData,iName,iPare);
		}
		private void initialize(byte[] data,int DataIndex,int NameIndex,int PareIndex){
			this.data=new byte[44];
			this.name=new byte[6];
			this.pare=new byte[6];
			if(data.Length<DataIndex+44)return;
			for(int i=0;i<44;i++){
				this.data[i]=data[i+DataIndex];
			}
			if(data.Length<NameIndex+6)return;
			if(data.Length<PareIndex+6)return;
			for(int i=0;i<6;i++){
				this.name[i]=data[i+NameIndex];
				this.pare[i]=data[i+PareIndex];
			}
		}
		private readonly int DataBase=11997;
		private readonly int PareBase=12261;
		private readonly int NameBase=12297;
		//=====================================
		//          properties
		//-------------------------------------
		[System.ComponentModel.Category("ポケモン"),System.ComponentModel.Description("ポケモンの管理番号")]
		public int Number{
			get{return this.data[0];}
			set{if(0<=value&&value<256)this.data[0]=(byte)value; else this.data[0]=(byte)0;}
		}
		[System.ComponentModel.Category("ポケモン"),System.ComponentModel.Description("ポケモンの種類")]
		public pokeKind Kind{
			get{return (pokeKind)this.data[0];}
			set{this.data[0]=(byte)value;}
		}
		[System.ComponentModel.Category("現在の状態"),System.ComponentModel.Description("残りの体力")]
		public int HP{
			get{return (data[1]<<8)+data[2];}
			set{
				if(typeof(int)!=value.GetType())return;
				this.data[1]=(byte)(value/256);this.data[2]=(byte)(value%256);
			}
		}
		[System.ComponentModel.Category("ポケモン"),System.ComponentModel.Description("タイプ１")]
		public pokeType Type1{
			get{return (pokeType)this.data[5];}
			set{this.data[5]=(byte)value;}
		}
		[System.ComponentModel.Category("ポケモン"),System.ComponentModel.Description("タイプ２")]
		public pokeType Type2{
			get{return (pokeType)this.data[6];}
			set{this.data[5]=(byte)value;}
		}
		[System.ComponentModel.Category("技"),System.ComponentModel.Description("わざ１")]
		public pokeWaza Waza1{
			get{return (pokeWaza)this.data[8];}
			set{this.data[8]=(byte)value;}
		}
		[System.ComponentModel.Category("技"),System.ComponentModel.Description("わざ２")]
		public pokeWaza Waza2{
			get{return (pokeWaza)this.data[9];}
			set{this.data[9]=(byte)value;}
		}
		[System.ComponentModel.Category("技"),System.ComponentModel.Description("わざ３")]
		public pokeWaza Waza3{
			get{return (pokeWaza)this.data[10];}
			set{this.data[10]=(byte)value;}
		}
		[System.ComponentModel.Category("技"),System.ComponentModel.Description("わざ４")]
		public pokeWaza Waza4{
			get{return (pokeWaza)this.data[11];}
			set{this.data[11]=(byte)value;}
		}
		[System.ComponentModel.Category("ID"),System.ComponentModel.Description("ID")]
		public int ID{
			get{return (data[12]<<8)+data[13];}
			set{
				if(typeof(int)!=value.GetType())return;
				this.data[12]=(byte)(value/256);this.data[3]=(byte)(value%256);
			}
		}
		[System.ComponentModel.Category("経験値"),System.ComponentModel.Description("ポケモンの経験値の大きさを示しています")]
		public int Experience{
			get{return data[14]<<16+data[15]<<8+data[16];}
			set{
				if(typeof(int)!=value.GetType())return;
				this.data[14]=(byte)(value/65536);
				this.data[15]=(byte)((value/256)%256);
				this.data[16]=(byte)(value%256);
			}
		}
		//=====================================
		//          enum
		//-------------------------------------
		public enum pokeType{
			ノーマル=0,
			かくとう=1,
			ひこう=2,
			どく=3,
			じめん=4,
			いわ=5,
			むし=7,
			ゴースト=8,
			はがね=9,
			ほのお=20,
			みず=21,
			くさ=22,
			でんき=23,
			エスパー=24,
			こおり=25,
			ドラゴン=26,
			あく=27
		}
		public enum pokeKind{
			不明=0, サイドン=1, ガルーラ=2, ニドラン雄=3, ピッピ=4, オニスズメ=5, ビリリダマ=6, ニドキング=7,
			ヤドラン=8, フシギソウ=9, ナッシー=10, ベロリンガ=11, タマタマ=12, ベトベター=13, ゲンガー=14, ニドラン雌=15,
			ニドクイン=16, カラカラ=17, サイホーン=18, ラプラス=19, ウインディー=20, ミュウ=21, ギャラドス=22, シェルダー=23,
			メノクラゲ=24, ゴース=25, ストライク=26, ヒトデマン=27, カメックス=28, カイロス=29, モンジャラ=30, ハッサム=31,
			ツボツボ=32, ガーディ=33, イワーク=34, オニドリル=35, ポッポ=36, ヤドン=37, ユンゲラー=38, ゴローン=39,
			ラッキー=40, ゴーリキー=41, バリヤード=42, サワムラー=43, エビワラー=44, アーボック=45, パラセクト=46, コダック=47,
			スリープ=48, ゴローニャ=49, ヘラクロス=50, ブーバー=51, ホウオウ=52, エレブー=53, レアコイル=54, ドガース=55,
			ニューラ=56, マンキー=57, パウワウ=58, ディグダ=59, ケンタロス=60, ヒメグマ=61, リングマ=62, マグマッグ=63,
			カモネギ=64, コンパン=65, カイリュー=66, マグカルゴ=67, ウリムー=68, イノムー=69, ドードー=70, ニョロモ=71,
			ルージュラ=72, ファイヤー=73, フリーザ=74, サンダー=75, メタモン=76, ニャース=77, クラブ=78, サニーゴ=79,
			テッポウオ=80, オクタン=81, ロコン=82, キュウコン=83, ピカチュウ=84, ライチュウ=85, デリバード=86, マンタイン=87,
			ミニリュウ=88, ハクリュウ=89, カブト=90, カブトプス=91, タッツー=92, シードラ=93, エアームド=94, デルビル=95,
			サンド=96, サンドパン=97, オムナイト=98, オムスター=99, プリン=100, プクリン=101, イーブイ=102, ブースター=103,
			サンダース=104, シャワーズ=105, ワンリキー=106, ズバット=107, アーボ=108, パラス=109, ニョロゾ=110, ニョロボン=111,
			ビードル=112, コクーン=113, スピアー=114, ヘルガー=115, ドードリオ=116, オコリザル=117, ダグドリオ=118, モルフォン=119,
			ジュゴン=120, キングドラ=121, ゴマゾウ=122, キャタピー=123, トランセル=124, バタフリー=125, カイリキー=126, ドンファン=127,
			ゴルダック=128, スリーパー=129, ゴルバット=130, ミュウツー=131, カビゴン=132, コイキング=133, ポリゴン=134, オドシシ=135,
			ベトベトン=136, ドーブル=137, キングラー=138, パルシェン=139, バルキー=140, マルマイン=141, ピクシー=142, マタドガス=143,
			ペルシアン=144, ガラガラ=145, カポエラー=146, ゴースト=147, ケーシィ=148, フーディン=149, ピジョン=150, ピジョット=151,
			スターミー=152, フシギダネ=153, フシギバナ=154, メノクラゲ重複=155, ムチュール=156, トサキント=157, アズマオウ=158, エレキッド=159,
			ブビィ=160, ミルタンク=161, ハピナス=162, ポニータ=163, ギャロップ=164, コラッタ=165, ラッタ=166, ニドリーノ=167,
			ニドリーナ=168, イシツブテ=169, ポリゴン重複=170, プテラ=171, ライコウ=172, コイル=173, エンテイ=174, スイクン=175,
			ヒトカゲ=176, ゼニガメ=177, リザード=178, カメール=179, リザードン=180, ヨーギラス=181, サナギラス=182, バンギラス=183,
			ルギア=184, ナゾノクサ=185, クサイハナ=186, ラフレシア=187, マダツボミ=188, ウツドン=189, ウツボット=190, チコリータ=191,
			ベイリーフ=192, メガニウム=193, ヒノアラシ=194, マグマラシ=195, バクフーン=196, ワニノコ=197, アリゲイツ=198, オーダイル=199,
			オタチ=200, オオタチ=201, ホーホー=202, ヨルノズク=203, レディバ=204, レディアン=205, イトマル=206, アリアドス=207, クロバット=208,
			チョンチー=209, ランターン=210, ピチュー=211, ピィ=212, ププリン=213, トゲピー=214, トゲチック=215,
			ネイティ=216, ネイティオ=217, メリープ=218, モココ=219, デンリュウ=220, キレイハナ=221, マリル=222, マリルリ=223,
			ウソッキー=224, ニョロトノ=225, ハネッコ=226, ポポッコ=227, ワタッコ=228, エイパム=229, ヒマナッツ=230, キマワリ=231,
			ヤンヤンマ=232, ウパー=233, ヌオー=234, エーフィ=235, ブラッキー=236, ヤミカラス=237, ヤドキング=238, ムウマ=239,
			アンノーン=240, ソーナンス=241, キリンリキ=242, クヌギダマ=243, フォトレス=244, ノコッチ=245, グライガー=246, ハガネール=247,
			ブルー=248, グランブル=249, ハリーセン=250, 不明1=251, 不明2=252, 不明ホウオウ=253, 不明3=254, 不明4 =255
		}
		public enum pokeWaza{
			w未定=0, wはたく=1, wからてチョップ=2, wおうふくビンタ=3, wれんぞくパンチ=4, wメガトンパンチ=5, wネコにこばん=6, wほのおのパンチ=7, 
			wれいとうパンチ=8, wかみなりパンチ=9, wひっかく=10, wはさむ=11, wハサミギロチン=12, wかまいたち=13, wつるぎのまい=14, wいあいぎり=15, 
			wかぜおこし=16, wつばさでうつ=17, wふきとばし=18, wそらをとぶ=19, wしめつける=20, wたたきつける=21, wつるのムチ=22, wふみつけ=23, 
			wにどげり=24, wメガトンキック=25, wとびげり=26, wまわしげり=27, wすなかけ=28, wずつき=29, wつのでつく=30, wみだれづき=31, 
			wつのドりル=32, wたいあたり=33, wのしかかり=34, wまきつく=35, wとっしん=36, wあばれる=37, wすてみタックル=38, wしっぽをふる=39, 
			wどくばり=40, wダブルニードル=41, wミサイルばり=42, wにらみつける=43, wかみつく=44, wなきごえ=45, wほえる=46, wうたう=47, 
			wちょうおんぱ=48, wソニックブーム=49, wかなしばり=50, wようかいえき=51, wひのこ=52, wかえんほうしゃ=53, wしろいきり=54, wみずでっぽう=55, 
			wハイドロポンプ=56, wなみのり=57, wれいとうビーム=58, wふぶき=59, wサイケこうせん=60, wバブルこうせん=61, wオーロラビーム=62, wはかいこうせん=63, 
			wつつく=64, wドりルくちばし=65, wじごくぐるま =66,wけたぐり=67, wカウンター=68, wちきゅうなげ=69, wかいりき=70, wすいとる=71, 
			wメガドレイン=72, wやどりぎのタネ=73, wせいちょう=74, wはっぱカッター=75, wソーラービーム=76, wどくのこな=77, wしびれごな=78, wねむりごな=79, 
			wはなびらのまい=80, wいとをはく=81, wりゅうのいかり=82, wほのおのうず=83, wでんきショック=84, w１０まんボルト=85, wでんじは=86, wかみなり=87, 
			wいわおとし=88, wじしん=89, wじわれ=90, wあなをほる=91, wどくどく=92, wねんりき=93, wサイコキネシス=94, wさいみんじゅつ=95, 
			wヨガのポーズ=96, wこうそくいどう=97, wでんこうせっか=98, wいかり=99, wテレポート=100, wナイトへッド=101, wものまね=102, wいやなおと=103, 
			wかげぶんしん=104, wじこさいせい=105, wかたくなる=106, wちいさくなる=107, wえんまく=108, wあやしいひかり=109, wからにこもる=110, wまるくなる=111, 
			wバりアー=112, wひかりのかべ=113, wくろいきり=114, wりフレクター=115, wきあいだめ=116, wがまん=117, wゆびをふる=118, wオウムがえし=119, 
			wじばく=120, wタマゴばくだん=121, wしたでなめる=122, wスモッグ=123, wへドロこうげき=124, wホネこんぼう=125, wだいもんじ=126, wたきのぼり=127, 
			wからではさむ=128, wスピードスター=129, wロケットずつき=130, wとげキャノン=131, wからみつく=132, wドわすれ=133, wスプーンまげ=134, wタマゴうみ=135, 
			wとびひざげり=136, wへびにらみ=137, wゆめくい=138, wどくガス=139, wたまなげ=140, wきゅうけつ=141, wあくまのキッス=142, wゴッドバード=143, 
			wへんしん=144, wあわ=145, wピヨピヨパンチ=146, wキノコのほうし=147, wフラッシュ=148, wサイコウェーブ=149, wはねる=150, wとける=151, 
			wクラブハンマー=152, wだいばくはつ=153, wみだれひっかき=154, wホネブーメラン=155, wねむる=156, wいわなだれ=157, wひっさつまえば=158, wかくばる=159, 
			wテクスチャー=160, wトライアタック=161, wいかりのまえば=162, wきりさく=163, wみがわり=164, wわるあがき=165			
		}
	}

}

