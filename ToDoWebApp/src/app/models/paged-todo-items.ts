import { ToDoItem } from "./todo-item";

export class PagedToDoItems{
    public currentPage!: number;
    public pageCount!: number;
    public pageSize!: number;

    public items: ToDoItem[] | undefined;
}