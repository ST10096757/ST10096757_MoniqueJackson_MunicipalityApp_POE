using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	// Basic Binary Search Tree to store service requests
	public class BinarySearchTree
	{
		public class TreeNode
		{
			public ServiceRequest Request { get; set; }
			public TreeNode Left { get; set; }
			public TreeNode Right { get; set; }

			public TreeNode(ServiceRequest request)
			{
				Request = request;
				Left = null;
				Right = null;
			}
		}

		private TreeNode root;

		public BinarySearchTree()
		{
			root = null;
		}

		// Insert a new request into the BST
		public void Insert(ServiceRequest request)
		{
			root = InsertRec(root, request);
		}

		private TreeNode InsertRec(TreeNode root, ServiceRequest request)
		{
			if (root == null)
			{
				root = new TreeNode(request);
				return root;
			}

			if (request.RequestId < root.Request.RequestId)
				root.Left = InsertRec(root.Left, request);
			else if (request.RequestId > root.Request.RequestId)
				root.Right = InsertRec(root.Right, request);

			return root;
		}

		// Find a request by RequestId
		public ServiceRequest Find(int requestId)
		{
			return FindRec(root, requestId);
		}

		private ServiceRequest FindRec(TreeNode root, int requestId)
		{
			if (root == null || root.Request.RequestId == requestId)
				return root?.Request;

			if (requestId < root.Request.RequestId)
				return FindRec(root.Left, requestId);

			return FindRec(root.Right, requestId);
		}

		// In-order traversal to get all requests in sorted order by ID
		public void InOrderTraversal(Action<ServiceRequest> action)
		{
			InOrderTraversalRec(root, action);
		}

		private void InOrderTraversalRec(TreeNode root, Action<ServiceRequest> action)
		{
			if (root != null)
			{
				InOrderTraversalRec(root.Left, action);
				action(root.Request);
				InOrderTraversalRec(root.Right, action);
			}
		}
	}
}
