import { Component, OnInit } from '@angular/core';
import { PagedToDoItems } from 'src/app/models/paged-todo-items';
import { ToDoItem } from 'src/app/models/todo-item';
import { ToDoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public toDos: PagedToDoItems | undefined;
  public newToDo: ToDoItem | undefined;

  public isCompletedFilter: boolean | undefined;
  public searchText: string = "";
  public pageSize: number = 5;

  constructor(private toDoService: ToDoService) { }

  ngOnInit(): void {
    this.searchToDos();
  }

  searchToDos(pageIndex: number = 1): void{
    if(pageIndex > 0){
      this.toDoService.saerchToDos(this.searchText, this.isCompletedFilter, pageIndex, this.pageSize).subscribe(
        (result) => {
          this.toDos = result;
          console.log(this.toDos);
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  changeIsCompletedFilter(state: boolean | undefined): void{
    if(this.isCompletedFilter != state){
      this.isCompletedFilter = state;
      this.searchToDos();
    }
  }

  search(): void{
    let searchTerm = this.searchText;
    setTimeout(() => {
        if (searchTerm == this.searchText) {
          this.searchToDos();
        }
    }, 200);
  }

  removeToDoItem(e: number): void{
    this.toDoService.deleteToDo(e).subscribe(
      (result) => {
        this.searchToDos();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  saveToDoItem(e: ToDoItem): void{
    if(e){
      if(e.id == 0){
        this.toDoService.addToDo(e).subscribe(
          (result) => {
            this.newToDo = undefined;
            this.searchToDos();
          },
          (error) => {
            console.log(error);
            this.searchToDos();
          }
        );
      }
      else{
        this.toDoService.modifyToDo(e).subscribe(
          (result) => {
    
          },
          (error) => {
            console.log(error);
            this.searchToDos();
          }
        );
      }
    }    
  }

  addToDo(): void{
    this.newToDo = new ToDoItem(0, "", false);
  }

  removeNewToDo(): void{
    this.newToDo = undefined;
  }

  prevPage(): void{
    if(this.toDos){
      this.searchToDos(this.toDos.currentPage - 1);
    }
  }

  nextPage(): void{
    if(this.toDos){
      this.searchToDos(this.toDos.currentPage + 1);
    }
  }

  changePageSize(pageSize: number): void{
    this.pageSize = pageSize;
    this.searchToDos();
  }
}
