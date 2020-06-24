using System;

namespace ImageResizer
{
    struct ImageSize
    {
        public int width, height;

        public ImageSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public override string ToString()
        {
            return string.Format("{0} x {1}", width, height);
        }
    }
}
