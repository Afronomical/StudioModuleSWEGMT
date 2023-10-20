/*
 *This script is used by the AI pathfinding algroithm, 
 *it optimizes finding the node that has the lowest fCost
 *
 *Written by Aaron
 */

using System;
using System.Collections;
using UnityEngine;

public class PathfindingHeap <T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemCount;

    public PathfindingHeap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];  // Get the first item
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    public void UpdateItem(T item)  // Lets you change the priority of an item in the heap
    {
        SortUp(item);  // Increase the priority
    }

    public int Count  // Returns how many items are in the heap
    {
        get
        {
            return currentItemCount;
        }
    }

    public bool Contains(T item)  // Checks whether an item is in the heap
    {
        if (item.HeapIndex < currentItemCount)
            return Equals(items[item.HeapIndex], item);
        else
            return false;
    }

    private void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentItemCount)
                {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                    Swap(item, items[swapIndex]);
                else
                    return;
            }
            else
                return;
        }
    }

    private void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)  // If this item has a lower fCost
                Swap(item, parentItem);  // Swap the positions of the two items
            else
                break;
        }
    }

    private void Swap(T itemA, T itemB)  // Swap the positions of two items in the heap
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        (itemA.HeapIndex, itemB.HeapIndex) = (itemB.HeapIndex, itemA.HeapIndex);  // Swap the heap indexes
    }

    public void Clear()
    {
        currentItemCount = 0;  // By setting this to 0, none of the functions will look through the heap
    }
}


public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}