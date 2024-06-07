export interface PaginatedResponse<T> {
  data: T[]
  pageSize: number
  currentPage: number
  totalCount: number
  nextPage: number | null
  previousPage: number | null
  totalPages: number
}
