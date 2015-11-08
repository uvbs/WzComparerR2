﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WzComparerR2.WzLib
{
    public class Wz_Uol
    {
        public Wz_Uol(string uol)
        {
            this.uol = uol;
        }

        private string uol;

        /// <summary>
        /// 获取或设置连接路径字符串。
        /// </summary>
        public string Uol
        {
            get { return uol; }
            set { uol = value; }
        }

        public Wz_Node HandleUol(Wz_Node currentNode)
        {
            if (currentNode == null || currentNode.ParentNode == null || string.IsNullOrEmpty(uol))
                return null;
            string[] dirs = this.uol.Split('/');
            currentNode = currentNode.ParentNode;

            bool outImg = false;

            foreach (string dir in dirs)
            {
                if (dir == "..")
                {
                    if (currentNode.ParentNode == null)
                    {
                        Wz_Image img = currentNode.GetValueEx<Wz_Image>(null);
                        if (img != null)
                        {
                            currentNode = img.OwnerNode.ParentNode;
                            outImg = true;
                        }
                        else
                        {
                            currentNode = null;
                        }
                    }
                    else
                    {
                        currentNode = currentNode.ParentNode;
                    }
                }
                else
                {
                    if (outImg)
                    {
                        currentNode = currentNode.FindNodeByPath(true, dir + ".img");
                        if (currentNode.GetValueEx<Wz_Image>(null) != null)
                        {
                            outImg = false;
                        }
                    }
                    else
                    {
                        currentNode = currentNode.FindNodeByPath(dir);
                    }
                }
                if (currentNode == null)
                    return null;
            }
            return currentNode;
        }
    }
}
