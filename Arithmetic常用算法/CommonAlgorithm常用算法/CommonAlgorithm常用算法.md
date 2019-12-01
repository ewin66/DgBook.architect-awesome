





# 常见排序算法及对应的时间复杂度和空间复杂度









# 常见排序算法及对应的时间复杂度和空间复杂度

2016-09-25 00:14:24[Gane_Cheng](https://me.csdn.net/Gane_Cheng)阅读数 75113                                                                                                    收藏                    

​                                分类专栏：                                                                                                            [                                             数据结构与算法                                        ](https://blog.csdn.net/gane_cheng/category_6434442.html)                                                                                                

​                                    

​                    [                     ](http://creativecommons.org/licenses/by-sa/4.0/)                                            版权声明：本文为博主原创文章，遵循[ CC 4.0 BY-SA ](http://creativecommons.org/licenses/by-sa/4.0/)版权协议，转载请附上原文出处链接和本声明。                                                                本文链接：https://blog.csdn.net/Gane_Cheng/article/details/52652705                                    

转载请注明出处：

http://blog.csdn.net/gane_cheng/article/details/52652705

http://www.ganecheng.tech/blog/52652705.html （浏览效果更好）

排序算法经过了很长时间的演变，产生了很多种不同的方法。对于初学者来说，对它们进行整理便于理解记忆显得很重要。每种算法都有它特定的使用场合，很难通用。因此，我们很有必要对所有常见的排序算法进行归纳。

排序大的分类可以分为两种：内排序和外排序。在排序过程中，全部记录存放在内存，则称为内排序，如果排序过程中需要使用外存，则称为外排序。下面讲的排序都是属于内排序。

内排序有可以分为以下几类：

　　(1)、插入排序：直接插入排序、二分法插入排序、希尔排序。

　　(2)、选择排序：直接选择排序、堆排序。

　　(3)、交换排序：冒泡排序、快速排序。

　　(4)、归并排序

　　(5)、基数排序

**表格版**

| 排序方法     | 时间复杂度（平均） | 时间复杂度（最坏) | 时间复杂度（最好) | 空间复杂度 | 稳定性 | 复杂性 |
| ------------ | ------------------ | ----------------- | ----------------- | ---------- | ------ | ------ |
| 直接插入排序 | *O*(*n*2)          |                   |                   |            |        |        |

|      | *O*(*n*2) |
| ---- | --------- |
|      |           |

|      | *O*(*n*) |
| ---- | -------- |
|      |          |

|      | *O*(1) |
| ---- | ------ |
|      |        |

|          | 稳定                  | 简单 |
| -------- | --------------------- | ---- |
| 希尔排序 | *O*(*n**l**o**g*2*n*) |      |

|      | *O*(*n*2) |
| ---- | --------- |
|      |           |

|      | *O*(*n*) |
| ---- | -------- |
|      |          |

|      | *O*(1) |
| ---- | ------ |
|      |        |

|              | 不稳定    | 较复杂 |
| ------------ | --------- | ------ |
| 直接选择排序 | *O*(*n*2) |        |

|      | *O*(*n*2) |
| ---- | --------- |
|      |           |

|      | *O*(*n*2) |
| ---- | --------- |
|      |           |

|      | *O*(1) |
| ---- | ------ |
|      |        |

|        | 不稳定                | 简单 |
| ------ | --------------------- | ---- |
| 堆排序 | *O*(*n**l**o**g*2*n*) |      |

|      | *O*(*n**l**o**g*2*n*) |
| ---- | --------------------- |
|      |                       |

|      | *O*(*n**l**o**g*2*n*) |
| ---- | --------------------- |
|      |                       |

|      | *O*(1) |
| ---- | ------ |
|      |        |

|          | 不稳定    | 较复杂 |
| -------- | --------- | ------ |
| 冒泡排序 | *O*(*n*2) |        |

|      | *O*(*n*2) |
| ---- | --------- |
|      |           |

|      | *O*(*n*) |
| ---- | -------- |
|      |          |

|      | *O*(1) |
| ---- | ------ |
|      |        |

|          | 稳定                  | 简单 |
| -------- | --------------------- | ---- |
| 快速排序 | *O*(*n**l**o**g*2*n*) |      |

|      | *O*(*n*2) |
| ---- | --------- |
|      |           |

|      | *O*(*n**l**o**g*2*n*) |
| ---- | --------------------- |
|      |                       |

|      | *O*(*n**l**o**g*2*n*) |
| ---- | --------------------- |
|      |                       |

|          | 不稳定                | 较复杂 |
| -------- | --------------------- | ------ |
| 归并排序 | *O*(*n**l**o**g*2*n*) |        |

|      | *O*(*n**l**o**g*2*n*) |
| ---- | --------------------- |
|      |                       |

|      | *O*(*n**l**o**g*2*n*) |
| ---- | --------------------- |
|      |                       |

|      | *O*(*n*) |
| ---- | -------- |
|      |          |

|          | 稳定              | 较复杂 |
| -------- | ----------------- | ------ |
| 基数排序 | *O*(*d*(*n*+*r*)) |        |

|      | *O*(*d*(*n*+*r*)) |
| ---- | ----------------- |
|      |                   |

|      | *O*(*d*(*n*+*r*)) |
| ---- | ----------------- |
|      |                   |

|      | *O*(*n*+*r*) |
| ---- | ------------ |
|      |              |

|      | 稳定 | 较复杂 |
| ---- | ---- | ------ |
|      |      |        |

------

**图片版**

![这里写图片描述](CommonAlgorithm常用算法.assets/20160924191851607)

------

## **① 插入排序**

•思想：每步将一个待排序的记录，按其顺序码大小插入到前面已经排序的字序列的合适位置，直到全部插入排序完为止。 
 •关键问题：在前面已经排好序的序列中找到合适的插入位置。 
 •方法： 
 –直接插入排序 
 –二分插入排序 
 –希尔排序

**(1)直接插入排序**（从后向前找到合适位置后插入）

1、基本思想：每步将一个待排序的记录，按其顺序码大小插入到前面已经排序的字序列的合适位置（从后向前找到合适位置后），直到全部插入排序完为止。

2、实例 
 ![这里写图片描述](CommonAlgorithm常用算法.assets/20160924195734790)

3、java实现

```
package DirectInsertSort;

public class DirectInsertSort
{

    public static void main(String[] args)
    {
        int[] a = { 49, 38, 65, 97, 76, 13, 27, 49, 78, 34, 12, 64, 1 };
        System.out.println("排序之前：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
        // 直接插入排序
        for (int i = 1; i < a.length; i++)
        {
            // 待插入元素
            int temp = a[i];
            int j;
            for (j = i - 1; j >= 0; j--)
            {
                // 将大于temp的往后移动一位
                if (a[j] > temp)
                {
                    a[j + 1] = a[j];
                }
                else
                {
                    break;
                }
            }
            a[j + 1] = temp;
        }
        System.out.println();
        System.out.println("排序之后：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }

    }

}
1234567891011121314151617181920212223242526272829303132333435363738394041424344
```

**(2)二分法插入排序**（按二分法找到合适位置插入）

1、基本思想：二分法插入排序的思想和直接插入一样，只是找合适的插入位置的方式不同，这里是按二分法找到合适的位置，可以减少比较的次数。

2、实例

![这里写图片描述](CommonAlgorithm常用算法.assets/20160924224427248)

3、java实现

```
package BinaryInsertSort;

public class BinaryInsertSort
{

    public static void main(String[] args)
    {
        int[] a = { 49, 38, 65, 97, 176, 213, 227, 49, 78, 34, 12, 164, 11, 18, 1 };
        System.out.println("排序之前：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
        // 二分插入排序
        sort(a);
        System.out.println();
        System.out.println("排序之后：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
    }

    private static void sort(int[] a)
    {
        for (int i = 0; i < a.length; i++)
        {
            int temp = a[i];
            int left = 0;
            int right = i - 1;
            int mid = 0;
            while (left <= right)
            {
                mid = (left + right) / 2;
                if (temp < a[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            for (int j = i - 1; j >= left; j--)
            {
                a[j + 1] = a[j];
            }
            if (left != i)
            {
                a[left] = temp;
            }
        }
    }

}
1234567891011121314151617181920212223242526272829303132333435363738394041424344454647484950515253545556
```

**(3)希尔排序**

1、基本思想：先取一个小于n的整数d1作为第一个增量，把文件的全部记录分成d1个组。所有距离为d1的倍数的记录放在同一个组中。先在各组内进行直接插入排序；然后，取第二个增量d2

```
package ShellSort;

public class ShellSort
{

    public static void main(String[] args)
    {
        int[] a = { 49, 38, 65, 97, 76, 13, 27, 49, 78, 34, 12, 64, 1 };
        System.out.println("排序之前：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
        // 希尔排序
        int d = a.length;
        while (true)
        {
            d = d / 2;
            for (int x = 0; x < d; x++)
            {
                for (int i = x + d; i < a.length; i = i + d)
                {
                    int temp = a[i];
                    int j;
                    for (j = i - d; j >= 0 && a[j] > temp; j = j - d)
                    {
                        a[j + d] = a[j];
                    }
                    a[j + d] = temp;
                }
            }
            if (d == 1)
            {
                break;
            }
        }
        System.out.println();
        System.out.println("排序之后：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }

    }

}
1234567891011121314151617181920212223242526272829303132333435363738394041424344454647
```

## **② 选择排序**

•思想：每趟从待排序的记录序列中选择关键字最小的记录放置到已排序表的最前位置，直到全部排完。 
 •关键问题：在剩余的待排序记录序列中找到最小关键码记录。 
 •方法： 
 –直接选择排序 
 –堆排序

**(1)直接选择排序**

1、基本思想：在要排序的一组数中，选出最小的一个数与第一个位置的数交换；然后在剩下的数当中再找最小的与第二个位置的数交换，如此循环到倒数第二个数和最后一个数比较为止。

2、实例

![这里写图片描述](CommonAlgorithm常用算法.assets/20160924225757124)

3、java实现

```csharp
package DirectSelectSort;

public class DirectSelectSort
{
    public static void main(String[] args)
    {
        int[] a = { 49, 38, 65, 97, 76, 13, 27, 49, 78, 34, 12, 64, 1, 8 };
        System.out.println("排序之前：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
        // 直接选择排序
        for (int i = 0; i < a.length; i++)
        {
            int min = a[i];
            int n = i; // 最小数的索引
            for (int j = i + 1; j < a.length; j++)
            {
                if (a[j] < min)
                { // 找出最小的数
                    min = a[j];
                    n = j;
                }
            }
            a[n] = a[i];
            a[i] = min;
        }
        System.out.println();
        System.out.println("排序之后：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
    }
}
```

**(2)堆排序**

1、基本思想：

　　堆排序是一种树形选择排序，是对直接选择排序的有效改进。

　　堆的定义下：具有n个元素的序列  （h1,h2,…,hn),当且仅当满足（hi>=h2i,hi>=2i+1）或（hi<=h2i,hi<=2i+1）  (i=1,2,…,n/2)时称之为堆。在这里只讨论满足前者条件的堆。由堆的定义可以看出，堆顶元素（即第一个元素）必为最大项（大顶堆）。完全二叉树可以很直观地表示堆的结构。堆顶为根，其它为左子树、右子树。

　　思想：初始时把要排序的数的序列看作是一棵顺序存储的二叉树，调整它们的存储序，使之成为一个堆，这时堆的根节点的数最大。然后将根节点与堆的最后一个节点交换。然后对前面(n-1)个数重新调整使之成为堆。依此类推，直到只有两个节点的堆，并对它们作交换，最后得到有n个节点的有序序列。从算法描述来看，堆排序需要两个过程，一是建立堆，二是堆顶与堆的最后一个元素交换位置。所以堆排序有两个函数组成。一是建堆的渗透函数，二是反复调用渗透函数实现排序的函数。

2、实例

初始序列：46,79,56,38,40,84

建堆：

![这里写图片描述](CommonAlgorithm常用算法.assets/20160924232259440)

交换，从堆中踢出最大数

![这里写图片描述](CommonAlgorithm常用算法.assets/20160924232343191)

依次类推：最后堆中剩余的最后两个结点交换，踢出一个，排序完成。

3、java实现

```csharp
package HeapSort;

import java.util.Arrays;

public class HeapSort
{
    public static void main(String[] args)
    {
        int[] a = { 49, 38, 65, 97, 76, 13, 27, 49, 78, 34, 12, 64 };
        int arrayLength = a.length;
        // 循环建堆
        for (int i = 0; i < arrayLength - 1; i++)
        {
            // 建堆
            buildMaxHeap(a, arrayLength - 1 - i);
            // 交换堆顶和最后一个元素
            swap(a, 0, arrayLength - 1 - i);
            System.out.println(Arrays.toString(a));
        }
    }

    // 对data数组从0到lastIndex建大顶堆
    public static void buildMaxHeap(int[] data, int lastIndex)
    {
        // 从lastIndex处节点（最后一个节点）的父节点开始
        for (int i = (lastIndex - 1) / 2; i >= 0; i--)
        {
            // k保存正在判断的节点
            int k = i;
            // 如果当前k节点的子节点存在
            while (k * 2 + 1 <= lastIndex)
            {
                // k节点的左子节点的索引
                int biggerIndex = 2 * k + 1;
                // 如果biggerIndex小于lastIndex，即biggerIndex+1代表的k节点的右子节点存在
                if (biggerIndex < lastIndex)
                {
                    // 若果右子节点的值较大
                    if (data[biggerIndex] < data[biggerIndex + 1])
                    {
                        // biggerIndex总是记录较大子节点的索引
                        biggerIndex++;
                    }
                }
                // 如果k节点的值小于其较大的子节点的值
                if (data[k] < data[biggerIndex])
                {
                    // 交换他们
                    swap(data, k, biggerIndex);
                    // 将biggerIndex赋予k，开始while循环的下一次循环，重新保证k节点的值大于其左右子节点的值
                    k = biggerIndex;
                }
                else
                {
                    break;
                }
            }
        }
    }

    // 交换
    private static void swap(int[] data, int i, int j)
    {
        int tmp = data[i];
        data[i] = data[j];
        data[j] = tmp;
    }
}
```

## **③ 交换排序**

**(1)冒泡排序**

1、基本思想：在要排序的一组数中，对当前还未排好序的范围内的全部数，自上而下对相邻的两个数依次进行比较和调整，让较大的数往下沉，较小的往上冒。即：每当两相邻的数比较后发现它们的排序与排序要求相反时，就将它们互换。

2、实例

![这里写图片描述](CommonAlgorithm常用算法.assets/20160924234332642)

3、java实现

```csharp
package BubbleSort;

public class BubbleSort
{
    public static void main(String[] args)
    {
        int[] a = { 49, 38, 65, 97, 76, 13, 27, 49, 78, 34, 12, 64, 1, 8 };
        System.out.println("排序之前：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
        // 冒泡排序
        for (int i = 0; i < a.length; i++)
        {
            for (int j = 0; j < a.length - i - 1; j++)
            {
                // 这里-i主要是每遍历一次都把最大的i个数沉到最底下去了，没有必要再替换了
                if (a[j] > a[j + 1])
                {
                    int temp = a[j];
                    a[j] = a[j + 1];
                    a[j + 1] = temp;
                }
            }
        }
        System.out.println();
        System.out.println("排序之后：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
    }
}
```

**(2)快速排序**

1、基本思想：选择一个基准元素,通常选择第一个元素或者最后一个元素,通过一趟扫描，将待排序列分成两部分,一部分比基准元素小,一部分大于等于基准元素,此时基准元素在其排好序后的正确位置,然后再用同样的方法递归地排序划分的两部分。

2、实例

![这里写图片描述](CommonAlgorithm常用算法.assets/20160924234753097)

3、java实现

```csharp
package QuickSort;

public class QuickSort
{
    public static void main(String[] args)
    {
        int[] a = { 49, 38, 65, 97, 76, 13, 27, 49, 78, 34, 12, 64, 1, 8 };
        System.out.println("排序之前：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
        // 快速排序
        quick(a);
        System.out.println();
        System.out.println("排序之后：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
    }

    private static void quick(int[] a)
    {
        if (a.length > 0)
        {
            quickSort(a, 0, a.length - 1);
        }
    }

    private static void quickSort(int[] a, int low, int high)
    {
        if (low < high)
        { // 如果不加这个判断递归会无法退出导致堆栈溢出异常
            int middle = getMiddle(a, low, high);
            quickSort(a, 0, middle - 1);
            quickSort(a, middle + 1, high);
        }
    }

    private static int getMiddle(int[] a, int low, int high)
    {
        int temp = a[low];// 基准元素
        while (low < high)
        {
            // 找到比基准元素小的元素位置
            while (low < high && a[high] >= temp)
            {
                high--;
            }
            a[low] = a[high];
            while (low < high && a[low] <= temp)
            {
                low++;
            }
            a[high] = a[low];
        }
        a[low] = temp;
        return low;
    }
}
```

## **④ 归并排序**

1、基本思想:归并（Merge）排序法是将两个（或两个以上）有序表合并成一个新的有序表，即把待排序序列分为若干个子序列，每个子序列是有序的。然后再把有序子序列合并为整体有序序列。

2、实例

![这里写图片描述](CommonAlgorithm常用算法.assets/20160924235409099)

3、java实现

```csharp
package MergeSort;

import java.util.Arrays;

public class MergeSort
{
    /**
     * 归并排序 简介:将两个（或两个以上）有序表合并成一个新的有序表
     * 即把待排序序列分为若干个子序列，每个子序列是有序的。然后再把有序子序列合并为整体有序序列 时间复杂度为O(nlogn) 稳定排序方式
     * 
     * @param nums
     *            待排序数组
     * @return 输出有序数组
     */
    public static int[] sort(int[] nums, int low, int high)
    {
        int mid = (low + high) / 2;
        if (low < high)
        {
            // 左边
            sort(nums, low, mid);
            // 右边
            sort(nums, mid + 1, high);
            // 左右归并
            merge(nums, low, mid, high);
        }
        return nums;
    }

    public static void merge(int[] nums, int low, int mid, int high)
    {
        int[] temp = new int[high - low + 1];
        int i = low;// 左指针
        int j = mid + 1;// 右指针
        int k = 0;

        // 把较小的数先移到新数组中
        while (i <= mid && j <= high)
        {
            if (nums[i] < nums[j])
            {
                temp[k++] = nums[i++];
            }
            else
            {
                temp[k++] = nums[j++];
            }
        }

        // 把左边剩余的数移入数组
        while (i <= mid)
        {
            temp[k++] = nums[i++];
        }

        // 把右边边剩余的数移入数组
        while (j <= high)
        {
            temp[k++] = nums[j++];
        }

        // 把新数组中的数覆盖nums数组
        for (int k2 = 0; k2 < temp.length; k2++)
        {
            nums[k2 + low] = temp[k2];
        }
    }

    // 归并排序的实现
    public static void main(String[] args)
    {

        int[] nums = { 2, 7, 8, 3, 1, 6, 9, 0, 5, 4 };

        MergeSort.sort(nums, 0, nums.length - 1);
        System.out.println(Arrays.toString(nums));
    }
}
c
```

## **⑤ 基数排序**

1、基本思想：将所有待比较数值（正整数）统一为同样的数位长度，数位较短的数前面补零。然后，从最低位开始，依次进行一次排序。这样从最低位排序一直到最高位排序完成以后,数列就变成一个有序序列。

2、实例

![这里写图片描述](CommonAlgorithm常用算法.assets/20160925000311184)

3、java实现

```csharp
package BaseSort;

import java.util.*;

public class BaseSort
{

    public static void main(String[] args)
    {
        int[] a = { 49, 38, 65, 97, 176, 213, 227, 49, 78, 34, 12, 164, 11, 18, 1 };
        System.out.println("排序之前：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
        // 基数排序
        sort(a);
        System.out.println();
        System.out.println("排序之后：");
        for (int i = 0; i < a.length; i++)
        {
            System.out.print(a[i] + " ");
        }
    }

    private static void sort(int[] array)
    {
        // 找到最大数，确定要排序几趟
        int max = 0;
        for (int i = 0; i < array.length; i++)
        {
            if (max < array[i])
            {
                max = array[i];
            }
        }
        // 判断位数
        int times = 0;
        while (max > 0)
        {
            max = max / 10;
            times++;
        }
        // 建立十个队列
        List<ArrayList> queue = new ArrayList<ArrayList>();
        for (int i = 0; i < 10; i++)
        {
            ArrayList queue1 = new ArrayList();
            queue.add(queue1);
        }
        // 进行times次分配和收集
        for (int i = 0; i < times; i++)
        {
            // 分配
            for (int j = 0; j < array.length; j++)
            {
                int x = array[j] % (int) Math.pow(10, i + 1) / (int) Math.pow(10, i);
                ArrayList queue2 = queue.get(x);
                queue2.add(array[j]);
                queue.set(x, queue2);
            }
            // 收集
            int count = 0;
            for (int j = 0; j < 10; j++)
            {
                while (queue.get(j).size() > 0)
                {
                    ArrayList<Integer> queue3 = queue.get(j);
                    array[count] = queue3.get(0);
                    queue3.remove(0);
                    count++;
                }
            }
        }
    }

}
```

​                        