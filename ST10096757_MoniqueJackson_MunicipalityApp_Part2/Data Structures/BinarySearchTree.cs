using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Models;
using System;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	// Basic Binary Search Tree to store service requests
	public class BinarySearchTree
	{
		// Define the node of the tree (BSTNode)
		public class TreeNode
		{
			public ServiceRequest Request { get; set; }  // ServiceRequest object stored in each node
			public TreeNode Left { get; set; }            // Left child node
			public TreeNode Right { get; set; }           // Right child node

			public TreeNode(ServiceRequest request)
			{
				Request = request;
				Left = null;
				Right = null;
			}
		}

		private TreeNode root;  // The root of the binary search tree

		public BinarySearchTree()
		{
			root = null;  // Initialize the tree as empty
		}

		// Insert a new request into the BST
		public void Insert(ServiceRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request), "Cannot insert a null service request.");
			}

			root = InsertRec(root, request);  // Start the recursive insert from the root
		}

		private TreeNode InsertRec(TreeNode root, ServiceRequest request)
		{
			if (root == null)
			{
				root = new TreeNode(request);  // If we reach a null spot, insert the new request here
				return root;
			}

			if (request.RequestId < root.Request.RequestId)  // Go left if the new request ID is smaller
				root.Left = InsertRec(root.Left, request);
			else if (request.RequestId > root.Request.RequestId)  // Go right if the new request ID is larger
				root.Right = InsertRec(root.Right, request);

			return root;  // Return the (possibly updated) root node
		}

		// Find a request by RequestId
		public ServiceRequest Find(int requestId)
		{
			return FindRec(root, requestId);  // Start the search from the root
		}

		private ServiceRequest FindRec(TreeNode root, int requestId)
		{
			if (root == null || root.Request.RequestId == requestId)
				return root?.Request;  // If we found the request or reached the end, return the request

			// Search to the left or right depending on the RequestId comparison
			if (requestId < root.Request.RequestId)
				return FindRec(root.Left, requestId);

			return FindRec(root.Right, requestId);
		}

		// In-order traversal to get all requests in sorted order by RequestId
		public void InOrderTraversal(Action<ServiceRequest> action)
		{
			InOrderTraversalRec(root, action);  // Start the traversal from the root
		}

		private void InOrderTraversalRec(TreeNode root, Action<ServiceRequest> action)
		{
			if (root != null)
			{
				InOrderTraversalRec(root.Left, action);  // Traverse left subtree
				action(root.Request);                    // Process current node
				InOrderTraversalRec(root.Right, action); // Traverse right subtree
			}
		}

		// Pre-order traversal (alternative order)
		public void PreOrderTraversal(Action<ServiceRequest> action)
		{
			PreOrderTraversalRec(root, action);  // Start pre-order traversal
		}

		private void PreOrderTraversalRec(TreeNode root, Action<ServiceRequest> action)
		{
			if (root != null)
			{
				action(root.Request);                    // Process current node
				PreOrderTraversalRec(root.Left, action);  // Traverse left subtree
				PreOrderTraversalRec(root.Right, action); // Traverse right subtree
			}
		}

		// Post-order traversal (alternative order)
		public void PostOrderTraversal(Action<ServiceRequest> action)
		{
			PostOrderTraversalRec(root, action);  // Start post-order traversal
		}

		private void PostOrderTraversalRec(TreeNode root, Action<ServiceRequest> action)
		{
			if (root != null)
			{
				PostOrderTraversalRec(root.Left, action);  // Traverse left subtree
				PostOrderTraversalRec(root.Right, action); // Traverse right subtree
				action(root.Request);                      // Process current node
			}
		}

		// Get the smallest (minimum) request by RequestId
		public ServiceRequest FindMin()
		{
			return FindMinRec(root);  // Find the minimum from the root
		}

		private ServiceRequest FindMinRec(TreeNode root)
		{
			while (root?.Left != null)
			{
				root = root.Left;  // Keep going left until we reach the minimum node
			}

			return root?.Request;  // Return the smallest request
		}

		// Get the largest (maximum) request by RequestId
		public ServiceRequest FindMax()
		{
			return FindMaxRec(root);  // Find the maximum from the root
		}

		private ServiceRequest FindMaxRec(TreeNode root)
		{
			while (root?.Right != null)
			{
				root = root.Right;  // Keep going right until we reach the maximum node
			}

			return root?.Request;  // Return the largest request
		}

		// Display all requests (for debugging purposes)
		public void DisplayServiceRequestsInOrder()
		{
			InOrderTraversal(request =>
			{
				Console.WriteLine($"RequestId: {request.RequestId}, Description: {request.Description}, Status: {request.Status}");
			});
		}
	}
}
