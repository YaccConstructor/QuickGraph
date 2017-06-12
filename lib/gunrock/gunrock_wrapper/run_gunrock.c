/**
 * based on gunrock files for shared lib tests: shared_lib_cc.c, shared_lib_bfs.c, shared_lib_sssp.c
 */

#include <stdio.h>
#include <gunrock/gunrock.h>
#include "run_gunrock.h"

int* run_cc(int nodes_num, int edges_num, int* row_offsets, int* col_indices)
{
    ////////////////////////////////////////////////////////////////////////////
    struct GRTypes data_t;                 // data type structure
    data_t.VTXID_TYPE = VTXID_INT;         // vertex identifier
    data_t.SIZET_TYPE = SIZET_INT;         // graph size type
    data_t.VALUE_TYPE = VALUE_INT;         // attributes type

    struct GRSetup *config = InitSetup(1, NULL);   // gunrock configurations

    int num_nodes = nodes_num, num_edges = edges_num;

    struct GRGraph *grapho = (struct GRGraph*)malloc(sizeof(struct GRGraph));
    struct GRGraph *graphi = (struct GRGraph*)malloc(sizeof(struct GRGraph));
    graphi->num_nodes   = num_nodes;
    graphi->num_edges   = num_edges;
    graphi->row_offsets = (void*)row_offsets;
    graphi->col_indices = (void*)col_indices;

    gunrock_cc(grapho, graphi, config, data_t);

    ////////////////////////////////////////////////////////////////////////////
    int *labels = (int*)malloc(sizeof(int) * graphi->num_nodes);
    labels = (int*)grapho->node_value1;

    if (graphi) free(graphi);
    if (grapho) free(grapho);
    return labels;
}

int* run_bfs(int nodes_num, int edges_num, int* row_offsets, int* col_indices)
{
    ////////////////////////////////////////////////////////////////////////////
    struct GRTypes data_t;                 // data type structure
    data_t.VTXID_TYPE = VTXID_INT;         // vertex identifier
    data_t.SIZET_TYPE = SIZET_INT;         // graph size type
    data_t.VALUE_TYPE = VALUE_INT;         // attributes type
    int srcs[1] = {0};

    struct GRSetup *config = InitSetup(1, srcs);   // gunrock configurations

    int num_nodes = nodes_num, num_edges = edges_num;

    struct GRGraph *grapho = (struct GRGraph*)malloc(sizeof(struct GRGraph));
    struct GRGraph *graphi = (struct GRGraph*)malloc(sizeof(struct GRGraph));
    graphi->num_nodes   = num_nodes;
    graphi->num_edges   = num_edges;
    graphi->row_offsets = (void*)row_offsets;
    graphi->col_indices = (void*)col_indices;

    gunrock_bfs(grapho, graphi, config, data_t);

    ////////////////////////////////////////////////////////////////////////////
    int *labels = (int*)malloc(sizeof(int) * graphi->num_nodes);
    labels = (int*)grapho->node_value2;

    if (graphi) free(graphi);
    if (grapho) free(grapho);
    return labels;
}

int* run_sssp(int nodes_num, int edges_num, int src, int* row_offsets, int* col_indices, int* edge_values)
{
 ////////////////////////////////////////////////////////////////////////////
    struct GRTypes data_t;                 // data type structure
    data_t.VTXID_TYPE = VTXID_INT;         // vertex identifier
    data_t.SIZET_TYPE = SIZET_INT;         // graph size type
    data_t.VALUE_TYPE = VALUE_INT;         // attributes type
    int srcs[1] = {src};

    struct GRSetup *config = InitSetup(1, srcs);   // gunrock configurations

    int num_nodes = nodes_num, num_edges = edges_num;
    struct GRGraph *grapho = (struct GRGraph*)malloc(sizeof(struct GRGraph));
    struct GRGraph *graphi = (struct GRGraph*)malloc(sizeof(struct GRGraph));
    graphi->num_nodes   = num_nodes;
    graphi->num_edges   = num_edges;
    graphi->row_offsets = (void*)row_offsets;
    graphi->col_indices = (void*)col_indices;
    graphi->edge_values = (void*)edge_values;

    gunrock_sssp(grapho, graphi, config, data_t);

    ////////////////////////////////////////////////////////////////////////////
    int *labels = (int*)malloc(sizeof(int) * graphi->num_nodes);
    labels = (int*)grapho->node_value1;

    if (graphi) free(graphi);
    if (grapho) free(grapho);

    return labels;
}

void release_memory(int* pArray)
{
    if (pArray) free(pArray);
}