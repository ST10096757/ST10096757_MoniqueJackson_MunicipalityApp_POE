using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Managers;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.MVVM.Models;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Data_Structures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Data_Structures
{
	// Enum for Red-Black Tree node colors
	public class RedBlackTree<T> where T : IComparable<T>
	{
		private RedBlackTreeNode<T> root;

		public RedBlackTree()
		{
			root = null;
		}

		// Insert node and maintain Red-Black Tree properties
		public void Insert(T value)
		{
			RedBlackTreeNode<T> newNode = new RedBlackTreeNode<T>(value);
			InsertNode(root, newNode);
			FixInsert(newNode);
		}

		private void InsertNode(RedBlackTreeNode<T> root, RedBlackTreeNode<T> newNode)
		{
			if (root == null)
			{
				this.root = newNode;
				return;
			}

			var current = root;
			RedBlackTreeNode<T> parent = null;

			while (current != null)
			{
				parent = current;
				int comparison = newNode.Value.CompareTo(current.Value);

				if (comparison < 0)
					current = current.Left;
				else
					current = current.Right;
			}

			if (parent != null)
			{
				if (newNode.Value.CompareTo(parent.Value) < 0)
					parent.Left = newNode;
				else
					parent.Right = newNode;

				newNode.Parent = parent;
			}
		}

		// Fix Red-Black Tree properties after insertion (not fully implemented in the current code)
		private void FixInsert(RedBlackTreeNode<T> node)
		{
			// Balance the tree as per Red-Black Tree rules (color adjustments, rotations, etc.)
		}

		// Get all items from the tree in sorted order by RequestId
		public List<T> GetSortedItems()
		{
			List<T> sortedItems = new List<T>();
			InOrderTraversal(root, sortedItems);
			return sortedItems;
		}

		private void InOrderTraversal(RedBlackTreeNode<T> node, List<T> sortedItems)
		{
			if (node != null)
			{
				InOrderTraversal(node.Left, sortedItems);
				sortedItems.Add(node.Value);
				InOrderTraversal(node.Right, sortedItems);
			}
		}
		// Clear the tree
		public void Clear()
		{
			root = null;
		}
	}

	// Red-Black Tree Node class
	public class RedBlackTreeNode<T>
	{
		public T Value;
		public RedBlackTreeNode<T> Left;
		public RedBlackTreeNode<T> Right;
		public RedBlackTreeNode<T> Parent;
		public Color NodeColor;

		public RedBlackTreeNode(T value)
		{
			Value = value;
			Left = Right = Parent = null;
			NodeColor = Color.Red;
		}
	}

	// Enum for Red-Black Tree node colors
	public enum Color
	{
		Red,
		Black
	}

}