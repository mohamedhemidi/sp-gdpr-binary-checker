export interface IResponse<T> {
  IsSuccess: boolean;
  Message: string;
  Response: T;
  StackTrace: string;
  StatusCode: number;
}
