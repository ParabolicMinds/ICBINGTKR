using System;
using System.Collections.Generic;

/// <summary>
/// This contains Mapping types such as Texture data, Brushes, and the Map data.
/// </summary>

namespace ICBINGTKR
{
    class Entity
    {
        protected Dictionary<String, String> attributes = new Dictionary<String, String>();

        public Dictionary<String, String> Attributes
        {
            get { return attributes; }
        }

        public Entity() { }

        public Entity(IntVec3 origin)
        {
            attributes.Add("origin", origin.ToString());
        }

        public void AddAttribute(String key, String value)
        {
            attributes.Add(key, value);
        }

        public override String ToString()
        {
            String returnString = "{\n";
            foreach (KeyValuePair<String, String> entry in attributes)
            {
                returnString += "\"" + entry.Key + "\" \"" + entry.Value + "\"\n";
            }
            return returnString + "}\n";
        }
    }

    class BrushEntity : Entity
    {
        protected List<Brush> brushes = new List<Brush>();

        public void AddBrush(Brush b)
        {
            this.brushes.Add(b);
        }

        public void AddBrushes(IEnumerable<Brush> bs)
        {
            foreach (Brush b in bs)
            {
                this.brushes.Add(b);
            }
        }

        public override string ToString()
        {
            String returnString = "{\n";

            foreach (KeyValuePair<String, String> entry in attributes)
            {
                returnString += "\"" + entry.Key + "\" \"" + entry.Value + "\"\n";
            }

            int i = 0;
            foreach (Brush b in brushes)
            {
                returnString += "// brush " + i + "\n" + b;
                i++;
            }

            return returnString + "}\n";
        }
    }

    class WorldspawnEntity : BrushEntity
    {
        public WorldspawnEntity()
        {
            attributes.Add("classname", "worldspawn");
        }

        public WorldspawnEntity(List<Brush> bs)
            : this()
        {
            brushes = bs;
        }
    }

    class LightEntity : Entity
    {
        public LightEntity(IntVec3 origin, int intensity, Q3Color color)
            : base(origin)
        {
            attributes.Add("classname", "light");
            attributes.Add("light", intensity.ToString());
            attributes.Add("_color", color.ToString());
        }
    }

    class JAInfoPlayerDeathmatchEntity : Entity
    {
        public JAInfoPlayerDeathmatchEntity(IntVec3 origin)
            : base(origin)
        {
            attributes.Add("classname", "info_player_deathmatch");
        }
    }

    class BrushFace
    {
        public static Texture DEFAULT_TEXTURE = new Texture("radiant/notex");
        private IntPlane plane;
        private Texture texture;

        public BrushFace(IntVec3 a, IntVec3 b, IntVec3 c, Texture texture) : this(new IntPlane(a, b, c), texture) { }
        public BrushFace(IntVec3 a, IntVec3 b, IntVec3 c) : this(new IntPlane(a, b, c), BrushFace.DEFAULT_TEXTURE) { }
        public BrushFace(IntPlane plane) : this(plane, DEFAULT_TEXTURE) { }
        public BrushFace(IntPlane plane, Texture texture) { this.plane = plane; this.texture = texture; }

        public IntPlane Plane
        {
            get { return plane; }
            set { plane = value; }
        }

        public Texture Texture
        {
            get { return texture; }
            set { texture = value; }
        }
    }

    class Brush
    {
        private List<BrushFace> faceList = new List<BrushFace>();

        public Brush() {}
        public Brush(IEnumerable<BrushFace> faces) { faceList.AddRange(faces); }

        public void AddFace(BrushFace face) { faceList.Add(face); }
        public void AddFaces(IEnumerable<BrushFace> faces) { faceList.AddRange(faces); }

        public override string ToString()
        {
            string returnstring = "{\n";
            foreach (BrushFace face in faceList)
            {
                returnstring += face.Plane + " " + face.Texture + "\n";
            }
            return returnstring + "}\n";
        }
    }

    class Texture
    {
        public string txstring;
        public int xoff;
        public int yoff;
        public int txrot;
        public float xscale;
        public float yscale;
        public bool detailflag;
        public int detailint
        {
            get { return (this.detailflag) ? 1 : 0; }
            set { this.detailflag = (value == 1); }
        }
        public Texture(string txstring) : this(txstring, 0, 0, 0, 0.5f, 0.5f) { }
        public Texture(string txstring, int xoff, int yoff)
            : this(txstring, xoff, yoff, 0, 0.5f, 0.5f) { }
        public Texture(string txstring, float xscale, float yscale)
            : this(txstring, 0, 0, 0, xscale, yscale) { }
        public Texture(string txstring, int xoff, int yoff, float xscale, float yscale)
            : this(txstring, xoff, yoff, 0, xscale, yscale) { }
        public Texture(string txstring, int xoff, int yoff, int txrot, float xscale, float yscale)
        {
            this.txstring = txstring;
            this.xoff = xoff;
            this.yoff = yoff;
            this.txrot = txrot;
            this.xscale = xscale;
            this.yscale = yscale;
            this.detailflag = false;

        }
        public void SetDetail(bool val = true)
        {
            this.detailflag = val;
        }
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} 0 0", txstring, xoff, yoff, txrot, xscale, yscale, detailint);
        }
    }

    class Map
    {
        private List<Entity> entities = new List<Entity>();
        private String mapName;

        public String MapName
        {
            get { return mapName; }
        }

        public Map(String mapName, WorldspawnEntity ws)
            : this(mapName)
        {
            this.entities.Add(ws);
        }

        public Map(String mapName)
        {
            this.mapName = mapName;
        }

        public void AddEntity(Entity e)
        {
            this.entities.Add(e);
        }

        public override string ToString()
        {
            string returnString = "";
            int i = 0;
            foreach (Entity e in entities)
            {
                returnString += "// entity " + i + "\n" + e;
                i++;
            }
            return returnString;
        }
    }
}

