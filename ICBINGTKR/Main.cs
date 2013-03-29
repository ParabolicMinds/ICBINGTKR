using System;
using System.Collections.Generic;
using System.IO;

namespace ICBINGTKR
{
	class Brush {
		public static Texture defaultTexture = new Texture("gothic_block/blocks18c_3");
		private IntVec3 spoint;
		private IntVec3 epoint;
		private List<string> strlist = new List<string>();
		private List<Texture> texlist = new List<Texture>();
		private Texture globalTexture;
		public Brush (IntVec3 veca, IntVec3 vecb) : this(veca, vecb, defaultTexture) {}
		public Brush (IntVec3 veca, IntVec3 vecb, Texture tex) {
			this.spoint = veca;
			this.epoint = vecb;
			this.globalTexture = tex;
			for(int i=0;i<6;i++){
				this.texlist.Add(this.globalTexture);
			}
			this.strlist.Add("( "+this.spoint.x+" 0 0 ) ( "+this.spoint.x+" 1 0 ) ( "+this.spoint.x+" 0 1 ) ");
			this.strlist.Add("( "+this.epoint.x+" 0 0 ) ( "+this.epoint.x+" 0 1 ) ( "+this.epoint.x+" 1 0 ) ");
			this.strlist.Add("( 0 "+this.spoint.y+" 0 ) ( 0 "+this.spoint.y+" 1 ) ( 1 "+this.spoint.y+" 0 ) ");
			this.strlist.Add("( 0 "+this.epoint.y+" 0 ) ( 1 "+this.epoint.y+" 0 ) ( 0 "+this.epoint.y+" 1 ) ");
			this.strlist.Add("( 0 0 "+this.spoint.z+" ) ( 1 0 "+this.spoint.z+" ) ( 0 1 "+this.spoint.z+" ) ");
			this.strlist.Add("( 0 0 "+this.epoint.z+" ) ( 0 1 "+this.epoint.z+" ) ( 1 0 "+this.epoint.z+" ) ");
		}
		public void AddCuttingPlane (IntVec3 vec1, IntVec3 vec2, IntVec3 vec3) {
			this.strlist.Add(" "+vec1+" "+vec2+" "+vec3+" ");
			this.texlist.Add(this.globalTexture);
		}
		public void SetTexture(int index, Texture tex){
			this.texlist[index] = tex;
		}
		public void SetAllTextures(Texture tex) {
			for(int i=0;i<this.texlist.Count;i++){
				this.texlist[i] = tex;
			}
			this.globalTexture = tex;
		}
		public override string ToString () {
			string returnstring = "";
			for (int i=0;i<this.strlist.Count;i++) {
				returnstring += this.strlist[i]+this.texlist[i]+"\n";
			}
			return returnstring;
		}
	}

	class Texture {
		public string txstring;
		public int xoff;
		public int yoff;
		public int txrot;
		public float xscale;
		public float yscale;
		public bool detailflag;
		public int detailint{get{if(this.detailflag){return 1;}else{return 0;}}set{if(value==0){this.detailflag=false;}else{this.detailflag=true;}}}
		public Texture (string txstring) : this(txstring, 0, 0, 0, 0.5f, 0.5f) {}
		public Texture (string txstring, int xoff, int yoff) : this(txstring, xoff, yoff, 0, 0.5f, 0.5f) {}
		public Texture (string txstring, float xscale, float yscale) : this(txstring, 0, 0, 0, xscale, yscale) {}
		public Texture (string txstring, int xoff, int yoff, float xscale, float yscale) : this(txstring, xoff, yoff, 0, xscale, yscale) {}
		public Texture (string txstring, int xoff, int yoff, int txrot, float xscale, float yscale) {
			this.txstring = txstring;
			this.xoff = xoff;
			this.yoff = yoff;
			this.txrot = txrot;
			this.xscale = xscale;
			this.yscale = yscale;
			this.detailflag = false;
		}
		public void SetDetail(bool val = true) {
			this.detailflag = val;
		}
		public override string ToString() {
			return this.txstring+" "+this.xoff+" "+this.yoff+" "+this.txrot+" "+this.xscale+" "+this.yscale+" "+this.detailint+" 0 0";
		}
	}

	class IntVec3 {
		public int x;
		public int y;
		public int z;
		public IntVec3(int[] arry) : this(arry[0], arry[1], arry[2]) {}
		public IntVec3(int x, int y, int z){
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public override string ToString() {
			return "( "+this.x+" "+this.y+" "+this.z+" )";
		}
	}

	class Manager {
		private int brushnum = 0;
		private int entitynum = 0;
		private List<Brush> brushes = new List<Brush>();
		public void CreateBrushes () {
			Brush tbrush1 = new Brush(new IntVec3(64,64,64), new IntVec3(512,512,512));
			tbrush1.AddCuttingPlane(new IntVec3(64,64,128), new IntVec3(128,64,64), new IntVec3(64,128,64));
			brushes.Add(tbrush1);
		}	
		public void WriteMap () {
			StreamWriter newmap = new StreamWriter("generation.map");
			newmap.Write("// entity "+this.entitynum+" \n{ \n\"classname\" \"worldspawn\" \n");entitynum++;
			foreach (Brush b in brushes) {
				newmap.Write ("// brush "+this.brushnum+"\n");brushnum++;
				newmap.Write ("{\n"+b+"}\n");
			}
			newmap.Close();
		}
	}

	class MainClass {
		public static void Main (string[] args) {
			Manager mainman = new Manager();
			mainman.CreateBrushes();
			mainman.WriteMap();
		}
	}
}
