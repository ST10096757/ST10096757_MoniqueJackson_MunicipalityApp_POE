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
	// Represents a Red-Black Tree data structure for storing ordered values
	public class RedBlackTree<T> where T : IComparable<T>
	{
		// Root node of the tree
		private RedBlackTreeNode<T> root;
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Constrcutor
		/// </summary>
		public RedBlackTree()
		{
			root = null;
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Insert a new value into the tree and maintain Red-Black Tree properties
		/// </summary>
		/// <param name="value"></param>
		public void Insert(T value)
		{
			// Create a new node with the given value
			RedBlackTreeNode<T> newNode = new RedBlackTreeNode<T>(value);

			// Insert the new node into the tree
			InsertNode(root, newNode);

			// Fix Red-Black Tree properties after insertion (like balancing the tree, coloring, etc.)
			FixInsert(newNode);
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Recursive method to find the correct position for the new node and insert it
		/// </summary>
		/// <param name="root"></param>
		/// <param name="newNode"></param>
		private void InsertNode(RedBlackTreeNode<T> root, RedBlackTreeNode<T> newNode)
		{
			// If the tree is empty, the new node becomes the root
			if (root == null)
			{
				this.root = newNode;
				return;
			}

			var current = root;
			RedBlackTreeNode<T> parent = null;

			// Traverse the tree to find the correct spot for the new node
			while (current != null)
			{
				// Keep track of the parent node
				parent = current;

				// Compare the new value with the current node's value
				int comparison = newNode.Value.CompareTo(current.Value);

				// Move left if the new value is smaller, otherwise move right
				if (comparison < 0)
					current = current.Left;
				else
					current = current.Right;
			}

			// Insert the new node as the left or right child of its parent
			if (parent != null)
			{
				if (newNode.Value.CompareTo(parent.Value) < 0)
					parent.Left = newNode;
				else
					parent.Right = newNode;

				// Set the parent reference of the new node
				newNode.Parent = parent;
			}
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Fix the Red-Black Tree properties after inserting a new node
		/// (This part is incomplete, typically involves balancing and color changes)
		/// </summary>
		/// <param name="node"></param>
		private void FixInsert(RedBlackTreeNode<T> node)
		{
			// Balance the tree (color adjustments, rotations) to satisfy Red-Black Tree rules
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Get all items in the tree sorted in ascending order
		/// </summary>
		/// <returns></returns>
		public List<T> GetSortedItems()
		{
			List<T> sortedItems = new List<T>();

			// Perform an in-order traversal of the tree to get the items in sorted order
			InOrderTraversal(root, sortedItems);

			// Return the sorted items
			return sortedItems;
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Recursive in-order traversal of the tree to collect sorted items
		/// </summary>
		/// <param name="node"></param>
		/// <param name="sortedItems"></param>
		private void InOrderTraversal(RedBlackTreeNode<T> node, List<T> sortedItems)
		{
			if (node != null)
			{
				// Traverse the left subtree
				InOrderTraversal(node.Left, sortedItems);

				// Add the node's value to the sorted list
				sortedItems.Add(node.Value);

				// Traverse the right subtree
				InOrderTraversal(node.Right, sortedItems);
			}
		}

		/// <summary>
		/// Clear the tree by setting the root to null
		/// </summary>
		public void Clear()
		{
			root = null;
		}
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	/// <summary>
	/// Represents a node in the Red-Black Tree
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RedBlackTreeNode<T>
	{
		// Value stored in the node
		public T Value;

		// Left and right children of the node
		public RedBlackTreeNode<T> Left;
		public RedBlackTreeNode<T> Right;

		// Parent of the node
		public RedBlackTreeNode<T> Parent;

		// Color of the node (Red or Black)
		public Color NodeColor;

		// Constructor: Initializes a new node with the given value
		public RedBlackTreeNode(T value)
		{
			Value = value;
			Left = Right = Parent = null;
			NodeColor = Color.Red; // New nodes are initially red
		}
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	/// <summary>
	/// Enum for Red-Black Tree node colors (Red or Black)
	/// </summary>
	public enum Color
	{
		Red,
		Black
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
}
