using System;
using System.Collections.Generic;
using System.IO;

namespace ICBINGTKR
{
	class Brush {
		public IntVec3 spoint;
		public IntVec3 epoint;
		public List<string> strlist = new List<string>();
		public List<Texture> texlist = new List<Texture>();
		public Brush (IntVec3 veca, IntVec3 vecb) {
			this.spoint = veca;
			this.epoint = vecb;
			for(int i=0;i<6;i++){
				this.texlist.Add(new Texture("gothic_block/blocks18c_3"));
			}
			this.strlist.Add("( "+this.spoint.x+" 0 0 ) ( "+this.spoint.x+" 1 0 ) ( "+this.spoint.x+" 0 1 ) ");
			this.strlist.Add("( "+this.epoint.x+" 0 0 ) ( "+this.epoint.x+" 0 1 ) ( "+this.epoint.x+" 1 0 ) ");
			this.strlist.Add("( 0 "+this.spoint.y+" 0 ) ( 0 "+this.spoint.y+" 1 ) ( 1 "+this.spoint.y+" 0 ) ");
			this.strlist.Add("( 0 "+this.epoint.y+" 0 ) ( 1 "+this.epoint.y+" 0 ) ( 0 "+this.epoint.y+" 1 ) ");
			this.strlist.Add("( 0 0 "+this.spoint.z+" ) ( 1 0 "+this.spoint.z+" ) ( 0 1 "+this.spoint.z+" ) ");
			this.strlist.Add("( 0 0 "+this.epoint.z+" ) ( 0 1 "+this.epoint.z+" ) ( 1 0 "+this.epoint.z+" ) ");
		}
		public void AddCuttingPlane (IntVec3 vec1, IntVec3 vec2, IntVec3 vec3) {
			this.strlist.Add(" "+vec1.MapString()+" "+vec2.MapString()+" "+vec3.MapString()+" ");
			this.texlist.Add(new Texture("gothic_block/blocks18c_3"));
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
		public string MapString() {
			return this.txstring+" "+this.xoff+" "+this.yoff+" "+this.txrot+" "+this.xscale+" "+this.yscale+" "+this.detailint+" 0 0";
		}
	}

	class IntVec3 {
		public int x;
		public int y;
		public int z;
		public IntVec3(int x, int y, int z){
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public string MapString() {
			return "( "+this.x+" "+this.y+" "+this.z+" )";
		}
	}

	class MainClass {

		public static void Main (string[] args) {

			IntVec3 testvec = new IntVec3(3,4,5);
			Console.Write(testvec.MapString());

			StreamWriter newmap = new StreamWriter("generation.map");
			newmap.Write("Test");
			newmap.Close();

		}
	}
}
