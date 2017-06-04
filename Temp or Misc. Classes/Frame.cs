using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    class Frame //Specifically for the question I've been asked in my round 4 of the following interview: https://drive.google.com/drive/u/0/folders/0BymDsEFdpqFOeTZVN2FleWxYVXM
    {
        //using this class to represent all the objects in a document (document itself, images, viewer).
        //As we only need height (top and bottom pixels), we only keep track of top and bottom of each frame.
        //For example, if the number of top of an image is 100, it means the vertical starting point of the image is 100 pixels from the top of document.
        //This being said, the Document always has top=0.

        int _top;
        int _bottom;

        public int Top
        {
            get { return _top; }
        }
        public int Bottom
        {
            get { return _bottom; }
        }

        public Frame(int top, int bottom)
        {
            _top = top;
            _bottom = bottom;
        }

    }
    class PreLoadImages //creating a class for solving the question that was mentioned above
    {
        public PreLoadImages() { }

        public void PreLoad(Frame Doc, Frame viewer, List<Frame>Images) //prints the list of images that are being loaded
        {
            int start = (viewer.Top - 1000 > 0) ? viewer.Top - 1000 : 0;
            int end = (viewer.Bottom + 1000 < Doc.Bottom) ? viewer.Bottom + 1000 : Doc.Bottom;

            for(int i=0; i<Images.Count;i++)
            {
                if (Images[i].Top <= start || Images[i].Top <= end)
                    Console.WriteLine("Loading Image " + i + "({0},{1})",Images[i].Top,Images[i].Bottom);
                if (Images[i].Bottom >= viewer.Bottom)
                    break;
            }
        }
    }
}
