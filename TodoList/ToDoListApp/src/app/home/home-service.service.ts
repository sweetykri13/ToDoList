import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { IHome } from './IHome';

@Injectable({
  providedIn: 'root'
})
export class HomeServiceService {
  todolist: IHome[] = [];
  constructor(private http: HttpClient) { }
  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error");
  }
  getToDolIST(): Observable<IHome[]> {
    let tem = this.http.get<IHome[]>('https://localhost:7086/api/ToDoLIst/GetToDoList');
    return tem;
  }
  
  addToDoList(description: string): Observable<boolean> {
    const url = `https://localhost:7086/api/ToDoLIst/AddToDoList?discription=${description}`;
    return this.http.post<boolean>(url, {}).pipe(catchError(this.errorHandler));
  }
  editToDoList(id: number, description: string): Observable<boolean> {
    const url = `https://localhost:7086/api/ToDoLIst/UpdateToDoList?id=${id}&discription=${description}`;
    return this.http.patch<boolean>(url, {}).pipe(catchError(this.errorHandler));
  }
  DeleteToDoList(id: number): Observable<boolean> {
    const url = `https://localhost:7086/api/ToDoLIst/DeleteToDoList?id=${id}`;
    return this.http.delete<boolean>(url, {}).pipe(catchError(this.errorHandler));
  }
}
