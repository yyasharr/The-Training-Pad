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
        Frame doc;
        Frame viewer;
        Frame[] images;
        public PreLoadImages() { }
        public PreLoadImages(Frame DOC, Frame VIEWER, Frame[] IMAGES)
        {
            doc = DOC;
            viewer = VIEWER;
            images = IMAGES;
        }
        public void PreLoad() //prints the list of images that are being loaded
        {
            if (images.Length == 0 || images == null) return;

            int start = (viewer.Top - 1000 > 0) ? viewer.Top - 1000 : 0;
            int end = (viewer.Bottom + 1000 < doc.Bottom) ? viewer.Bottom + 1000 : doc.Bottom;

            PreLoad(0, images.Length - 1, start, end);
        }
        private void PreLoad(int first, int last, int start, int end)
        {
            if (first >= last) return;

            int mid = (first + last) / 2;

            if (end < images[mid].Top) //if viewer is completely above the mid image.
            {
                PreLoad(first, mid - 1, start, end); //go up
            }
            if (start > images[mid].Bottom) //if viewer is completely below the mid image.
            {
                PreLoad(mid+1, last, start, end); //go down
            }
            if (images[mid].Top >= start && images[mid].Top <= end)
            {
                Console.WriteLine("image " + mid + " is being loaded."); //load it!
                PreLoad(first, mid - 1, start, end); //go up
            }
            if (images[mid].Bottom >= start && images[mid].Bottom <= end)
            {
                Console.WriteLine("image " + mid + " is being loaded."); //load it!
                PreLoad(mid + 1, last, start, end); //go down
            }
            if(images[mid].Top>start && images[mid].Bottom<end) //the viewer completely surrounds the mid image
            {
                Console.WriteLine("image " + mid + " is being loaded."); //load it!
                PreLoad(first, mid - 1, start, end); //go up
                PreLoad(mid + 1, last, start, end); //go down
            }
        }
    }
}
//////////////////////////////////INITIALIZER//////////////////////////////////////////////////////////////////////
//Frame[] images = {new Frame(100,800), new Frame(1000,1500), new Frame(2500, 2800), new Frame(3300, 4000), new Frame(4800, 5000), new Frame(5100,6100), new Frame(6800, 7500) };
//PreLoadImages pli = new PreLoadImages(new Frame(0, 8000), new Frame(6400, 6401), images);
