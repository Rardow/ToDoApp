export class ToDoItem{
    public id!: number;
    public note!: string;
    public isCompleted!: boolean;

    constructor(id: number, note: string, isCompleted: boolean){
        this.id = id;
        this.note = note;
        this.isCompleted = isCompleted;
    }
}