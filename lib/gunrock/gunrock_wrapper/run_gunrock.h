extern "C" {
	int* run_cc(int nodes_num, int edges_num, int* row_offsets, int* col_indices);
	int* run_bfs(int nodes_num, int edges_num, int* row_offsets, int* col_indices);
	int* run_sssp(int nodes_num, int edges_num, int src, int* row_offsets, int* col_indices, int* edge_values);
	void release_memory(int* pArray);
}