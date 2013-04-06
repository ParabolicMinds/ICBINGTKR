using System;
using System.Collections.Generic;

namespace ICBINGTKR
{
	class Cell {
		private List<Brush> wall_list = new List<Brush>();
		public Cell (IntVec3 startcorner, int cell_length) {
			this.wall_list.Add(new Brush (startcorner, new IntVec3(startcorner.x+cell_length, startcorner.y+16,startcorner.z+384)));
			this.wall_list.Add(new Brush (new IntVec3(startcorner.x,startcorner.y+cell_length,startcorner.z), new IntVec3(startcorner.x+cell_length, startcorner.y-16,startcorner.z+384)));
			this.wall_list.Add(new Brush (startcorner, new IntVec3(startcorner.x+16, startcorner.y+cell_length,startcorner.z+384)));
			this.wall_list.Add(new Brush (new IntVec3(startcorner.x+cell_length,startcorner.y,startcorner.z), new IntVec3(startcorner.x-16, startcorner.y+cell_length,startcorner.z+384)));
		}
	}
	class Maze {
		public int scalefactor;
		public int scale_x;
		public int scale_y;
		public int cell_length;
		public Maze (int scalefactor, int scale_x, int scale_y) {
			
		}
	}
}

