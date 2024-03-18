export class ResultModel<T>{
  data?:T;
  errorMessages?: string[] ;
  isSuccessfull?: boolean;
  statusCode: number =200;
}