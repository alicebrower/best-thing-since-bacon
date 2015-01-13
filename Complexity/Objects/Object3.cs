using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Complexity.Util;

namespace Complexity.Objects {
    //Represent a 3 Dimentional Object that can be rendered
    public abstract class Object3 {
        protected byte[] triangles;
        protected double[] geometry, colors;

        private Vector<double> rot, trans;
        protected Matrix<double> geo, tri, col;

        protected void Init() {
            rot = DenseVector.OfArray(new Double[] {
                1, 0, 0
            });

            trans = DenseVector.OfArray(new Double[] {
                0, 0, 0
            });
        }

        //This method is in charge of perfomring all the logic & calculations
        //This probably needs to be improved
        public void Recalculate() {
            Matrix<double> _geo = Matrix<double>.Build.Dense(geo.RowCount, geo.ColumnCount);

            //Transform matrix
            _geo = MathUtil.RotateMatrix3(Global.GetTime(), Global.GetTime(), Global.GetTime(), geo);
            _geo = MathUtil.TranslateMatrix3(0, 0, 0, _geo);

            geometry = _geo.ToColumnWiseArray();
        }

        public void Draw() {
            Recalculate();

            //Draw
            GL.VertexPointer(3, VertexPointerType.Double, 0, geometry);
            GL.ColorPointer(4, ColorPointerType.Double, 0, colors);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, new Color4(255    , 0, 0, 255));
            GL.DrawElements(BeginMode.Triangles, 36, DrawElementsType.UnsignedByte, triangles);
        }
    }
}
