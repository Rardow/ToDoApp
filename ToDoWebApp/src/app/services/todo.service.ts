import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { Observable } from "rxjs";
import { ToDoItem } from "../models/todo-item";

@Injectable({
    providedIn: 'root'
  })
export class ToDoService{

  constructor(private http: HttpClient) { }

  //returns error or the desired todo-item
  getToDoItem(id: number): Observable<any>{
    return this.http.get<any>(environment.apiUrl + "ToDo/GetToDoById?id=" + id);
  }  

  //returns error or all of the to-dos
  getAllToDos(): Observable<any>{
    return this.http.get<any>(environment.apiUrl + "ToDo/GetAllToDos");
  }  

  //returns error or search results
  saerchToDos(searchText: string | undefined, isCompleted: boolean | undefined, pageIndex: number, pageSize: number): Observable<any>{
    let query = "?";

    if(searchText){
      query += "SearchText=" + searchText + "&";
    }
    if(isCompleted != undefined){
      query += "IsCompleted=" + isCompleted + "&";
    }
    query += "PageIndex=" + pageIndex + "&";
    query += "PageSize=" + pageSize;

    return this.http.get<any>(environment.apiUrl + "ToDo/SearchToDos" + query);
  }

  //returns error or the added todoitem with it's id
  addToDo(item: ToDoItem): Observable<any>{
    return this.http.put<any>(environment.apiUrl + "ToDo/AddToDo", item);
  }  

  //returns error or the modified todoitem with it's id
  modifyToDo(item: ToDoItem): Observable<any>{
    return this.http.post<any>(environment.apiUrl + "ToDo/ModifyToDo", item);
  }  

  //returns error or the deletion result
  deleteToDo(id: number): Observable<any>{
    return this.http.delete<any>(environment.apiUrl + "ToDo/RemoveToDo?id=" + id);
  }  
}