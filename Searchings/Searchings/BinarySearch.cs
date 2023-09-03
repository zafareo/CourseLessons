using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searchings
{
    public static class BinarySearch
    {

        public static char NextGreatestLetter(char[] letters, char target)
        {
            int left = 0;
            int right = letters.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (letters[mid] <= target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            if (left < letters.Length)
            {
                return letters[left];
            }
            else
            {
                return letters[0];
            }
        }

        public static int FindMin(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1;

            while (nums[left] > nums[right])
            {
                int mid = (left + right) / 2;

                if (nums[mid] > nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }

            return nums[left];
        }

        public static int Search(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] == target)
                {
                    return mid;
                }
                else if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return -1;
        }


        public static int SearchInsert(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] == target)
                {
                    return mid;
                }
                else if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return left;
        }

    }
}
