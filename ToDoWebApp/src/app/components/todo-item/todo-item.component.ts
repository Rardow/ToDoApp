import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { ToDoItem } from 'src/app/models/todo-item';
import { ToDoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.scss']
})
export class TodoItemComponent implements OnInit {

  @Input()
  public toDoItem!: ToDoItem;

  @Input()
  public isNew: boolean = false;

  @Output()
  public removeToDoItemEvent: EventEmitter<number> = new EventEmitter<number>();

  @Output()
  public saveToDoItemEvent: EventEmitter<ToDoItem> = new EventEmitter<ToDoItem>();

  constructor(private toDoService: ToDoService) { }

  ngOnInit(): void {
  }

  removeToDoItem(): void{
    if(!this.isNew){
      this.removeToDoItemEvent.next(this.toDoItem.id);
    }
  }

  addToDoItem(): void{
    this.saveToDoItemEvent.next(this.toDoItem);
  }

  modifyNote(): void{
    if(!this.isNew){
      let newNote = this.toDoItem.note;
      setTimeout(() => {
          if (newNote == this.toDoItem.note) {
            this.saveToDoItemEvent.next(this.toDoItem);
          }
      }, 200);
    }
  }

  modifyState(): void{
    if(!this.isNew){
      this.saveToDoItemEvent.next(this.toDoItem);
    }
  }
}
