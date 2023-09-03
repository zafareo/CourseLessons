using RabbitMQ.Client;
using Searchings.RabbitMQ;

namespace Searchings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Publisher.SendMessage();

            #region 744. Find Smallest Letter Greater Than Target

            char[] letters1 = { 'c', 'f', 'j' };
            char target1 = 'a';

            char result1 = BinarySearch.NextGreatestLetter(letters1, target1);
            Console.WriteLine(result1);  

            char[] letters2 = { 'c', 'f', 'j' };
            char target2 = 'c';
            char result2 = BinarySearch.NextGreatestLetter(letters2, target2);
            Console.WriteLine(result2);  

            char[] letters3 = { 'x', 'x', 'y', 'y' };
            char target3 = 'z';
            char result3 = BinarySearch.NextGreatestLetter(letters3, target3);
            Console.WriteLine(result3); 

            #endregion

            #region 153. Find Minimum in Rotated Sorted Array

            int[] nums1 = { 3, 4, 5, 1, 2 };
            int min1 = BinarySearch.FindMin(nums1);
            Console.WriteLine(min1);  

            int[] nums2 = { 4, 5, 6, 7, 0, 1, 2 };
            int min2 = BinarySearch.FindMin(nums2);
            Console.WriteLine(min2);  

            int[] nums3 = { 11, 13, 15, 17 };
            int min3 = BinarySearch.FindMin(nums3);
            Console.WriteLine(min3); 

            #endregion

            // Console.WriteLine(  );

            //int[] nums = new int[] { -1, 0, 3, 5, 9, 12 };
            //int target = 9;


            ////int result = BinarySearch.Search(nums, target);
            ////Console.WriteLine(result);  // Output: 4

            ////target = 2;
            ////result = BinarySearch.Search(nums, target);
            ////Console.WriteLine(result);


            //int result = BinarySearch.SearchInsert(nums, target);
            //Console.WriteLine(result);

            //target = 2;
            //result = BinarySearch.SearchInsert(nums, target);
            //Console.WriteLine(result);

            //target = 7;
            //result = BinarySearch.SearchInsert(nums, target);
            //Console.WriteLine(result);
        }
    }
}