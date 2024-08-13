import { Pagination } from "./Pagination";

export class PaginatedResult<T> {
  result: T | null | undefined;
  pagination: Pagination | undefined;
}
