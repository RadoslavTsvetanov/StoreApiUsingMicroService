#include <stdio.h>
#include <stdlib.h>

// Define a structure for the vector
typedef struct
{
    int *data;       // Pointer to the actual data
    size_t size;     // Current number of elements in the vector
    size_t capacity; // Current capacity of the vector
} Vector;

// Initialize the vector
void vector_init(Vector *vec, size_t initial_capacity)
{
    vec->data = (int *)malloc(initial_capacity * sizeof(int));
    if (vec->data == NULL)
    {
        fprintf(stderr, "Memory allocation failed.\n");
        exit(1);
    }
    vec->size = 0;
    vec->capacity = initial_capacity;
}

// Add an element to the end of the vector
void vector_push_back(Vector *vec, int value)
{
    if (vec->size == vec->capacity)
    {
        // If the vector is full, double its capacity
        vec->capacity *= 2;
        vec->data = (int *)realloc(vec->data, vec->capacity * sizeof(int));
        if (vec->data == NULL)
        {
            fprintf(stderr, "Memory allocation failed.\n");
            exit(1);
        }
    }
    vec->data[vec->size++] = value;
}

// Access an element in the vector
int vector_get(const Vector *vec, size_t index)
{
    if (index >= vec->size)
    {
        fprintf(stderr, "Index out of bounds.\n");
        exit(1);
    }
    return vec->data[index];
}

// Clean up the vector and free memory
void vector_free(Vector *vec)
{
    free(vec->data);
    vec->size = 0;
    vec->capacity = 0;
    vec->data = NULL;
}

int find_smallest_index(Vector *vec, int start)
{
    int index_smallest = start;
    for (int i = index_smallest; i < vec->size; i++)
    {
        index_smallest = vec->data[i] < vec->data[index_smallest] ? i : index_smallest;
    }
    return index_smallest;
}

void InsertSort(Vector *vec)
{
    for (int i = 0; i < vec->size; i++)
    {
        int index = i;
        int smallest = find_smallest_index(vec, index);
        int temp = vec->data[index];
        vec->data[index] = vec->data[smallest];
        vec->data[smallest] = temp;
    }
}

void Big_sort(Vector *vec1, Vector *vec2, Vector *vec3)
{
    int vec1_indexer = 0;
    int vec2_indexer = 0;
    for (int i = 0; i < 20; i++)
    {
        if (vec1->data[vec1_indexer] < vec2->data[vec2_indexer])
        {
            vec3->data[i] = vec1->data[vec1_indexer];
            vec1_indexer++;
        }
        else
        {
            vec3->data[i] = vec2->data[vec2_indexer];
            vec2_indexer++;
        }
    }
}

int main()
{
    Vector myVector;
    vector_init(&myVector, 10);
    int arr[] = {1, 5, 3, 7, 4, 6, 7, 4, 3, 3};

    for (int i = 0; i < 10; i++)
    {
        vector_push_back(&myVector, arr[i]);
    }
    InsertSort(&myVector);

    for (size_t i = 0; i < myVector.size; i++)
    {
        printf("%d ", vector_get(&myVector, i));
    }

    Vector myVector2;
    vector_init(&myVector2, 10);
    int arr1[] = {3, 1, 4, 2, 10, 9, 5, 2, 0, 3};

    for (int i = 0; i < 10; i++)
    {
        vector_push_back(&myVector2, arr1[i]);
    }
    InsertSort(&myVector2);

    for (size_t i = 0; i < myVector2.size; i++)
    {
        printf("%d ", vector_get(&myVector2, i));
    }

    printf("----------------------------------------------------------------\n");
    Vector myVector3;
    vector_init(&myVector3, 20);
    Big_sort(&myVector, &myVector2, &myVector3);
    myVector3.size = 20;
    for (size_t i = 0; i < myVector3.size; i++)
    {
        printf("%d ", vector_get(&myVector3, i));
    }
    return 0;
}
