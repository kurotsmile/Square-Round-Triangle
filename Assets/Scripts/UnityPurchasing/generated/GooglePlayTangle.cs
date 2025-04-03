// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("93R6dUX3dH9393R0dZL+w3BPIafGBvBjQWYx58XJQRzS/CBXFXFQnKLBWOJAc26qHi5dPM7YV0Wvm0u3yZs/TRAhVYn6GpT8rn4k7zktbzPuX7KOJ6mDH2CS9eYX1ob5VyzWdY9L/xEfXn35cQLHjLGcGzgOzHKM8ODUsQEQPuc1cuw5fWbQ/PhEYa5hY0NhkgdFw9YOuvulyTHN7AVheluG2Jsg4T3cBR5Xfbknos0imDIg8+xbb6eLbp3HeAlh61tBF0fictwIG5sfotL9ipQhMVsFxZDg0uOGsbmAlxzy+ym9iWGwaQtOcm1Do8CqRfd0V0V4c3xf8z3zgnh0dHRwdXYP8VKT+UcyzPp+DrD0TMp8dbvn9WJ6XkyeVWHrand2dHV0");
        private static int[] order = new int[] { 1,2,8,3,4,9,7,9,10,12,11,12,12,13,14 };
        private static int key = 117;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
