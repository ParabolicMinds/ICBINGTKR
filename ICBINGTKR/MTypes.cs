using System;
using System.Collections.Generic;

/// <summary>
/// This contains Mapping types such as Texture data, Brushes, and the Map data.
/// </summary>

namespace ICBINGTKR
{
	class Brush {
		public static Texture defaultTexture = new Texture("gothic_block/blocks18c_3");
		private IntVec3 spoint;
		private IntVec3 epoint;
		private List<IntPlane> planelist = new List<IntPlane>();
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
			this.planelist.Add(new IntPlane(this.spoint.x,0,0,this.spoint.x,1,0,this.spoint.x,0,1));
			this.planelist.Add(new IntPlane(this.epoint.x,0,0,this.epoint.x,0,1,this.epoint.x,1,0));
			this.planelist.Add(new IntPlane(0,this.spoint.y,0,0,this.spoint.y,1,1,this.spoint.y,0));
			this.planelist.Add(new IntPlane(0,this.epoint.y,0,1,this.epoint.y,0,0,this.epoint.y,1));
			this.planelist.Add(new IntPlane(0,0,this.spoint.z,1,0,this.spoint.z,0,1,this.spoint.z));
			this.planelist.Add(new IntPlane(0,0,this.epoint.z,0,1,this.epoint.z,1,0,this.epoint.z));
		}
		public void AddCuttingPlane (IntPlane plane1) {AddCuttingPlane(plane1.VectorA, plane1.VectorB, plane1.VectorC);}
		public void AddCuttingPlane (IntVec3 vec1, IntVec3 vec2, IntVec3 vec3) {
			this.planelist.Add(new IntPlane(vec1,vec2,vec3));
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
			for (int i=0;i<this.planelist.Count;i++) {
				returnstring += this.planelist[i]+" "+this.texlist[i]+"\n";
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
	
	class Map {
		private int brushnum = 0;
		private int entitynum = 0;
		private List<Brush> brushes = new List<Brush>();
		public readonly string mapname;
		public Map (string mapname) {
			this.mapname = mapname;
		}
		public override string ToString () {
			string returnstring = "";
			returnstring += "// entity "+this.entitynum+" \n{ \n\"classname\" \"worldspawn\" \n";entitynum++;
			foreach (Brush b in brushes) {
				returnstring += "// brush "+this.brushnum+"\n";brushnum++;
				returnstring += "{\n"+b+"}\n";
			}
			returnstring += "}\n";
			return returnstring;
		}
		public Brush NewBrush (IntVec3 veca, IntVec3 vecb) {
			Brush tbrush = new Brush(veca, vecb);
			this.brushes.Add(tbrush);
			return tbrush;
		}
	}
}

