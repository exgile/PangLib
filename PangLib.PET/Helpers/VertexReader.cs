using System;
using System.Collections.Generic;
using System.IO;
using PangLib.PET.DataModels;

namespace PangLib.PET.Helpers
{
    static class VertexReader
    {
        public static List<Vertex> ReadAllVertices(BinaryReader sectionReader)
        {
            List<Vertex> Vertices = new List<Vertex>();

            uint vertexCount = sectionReader.ReadUInt32();

            for (int i = 0; i < vertexCount; i++)
            {
                Vertex vertex = new Vertex();

                vertex.X = sectionReader.ReadSingle();
                vertex.Y = sectionReader.ReadSingle();
                vertex.Z = sectionReader.ReadSingle();

                sbyte fullWeight = 0;
                int readCount = 0;

                while (fullWeight != 255 && readCount < 2)
                {
                    BoneInformation boneInformation = new BoneInformation();

                    sbyte weight = sectionReader.ReadSByte();

                    boneInformation.Weight = weight;
                    fullWeight += weight;

                    boneInformation.BoneID = sectionReader.ReadSByte();

                    vertex.BoneInformation.Add(boneInformation);
                    readCount += 1;
                }

                Vertices.Add(vertex);
            }

            return Vertices;
        }
    }
}
