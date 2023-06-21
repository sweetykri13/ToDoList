import { Component } from '@angular/core';
import { HomeServiceService } from './home-service.service';
import { IHome } from './IHome';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  showButtons = true;
  home: IHome[] = [];
  showErr: boolean | undefined;
  successMessage: string | undefined;
  id: number | undefined;
  editId: number | undefined;
  description: string | undefined;

  constructor(private _todolist: HomeServiceService) { }

  ngOnInit() {
    this.getAllList();
  }
  cancelUpdate() {
    this.editId = undefined;
    this.description = undefined;
  }
  getAllList() {
    this._todolist.getToDolIST().subscribe({
      next: (data: IHome[]) => {
        this.home = data;
        if (this.home.length === 0) {
          this.showErr = true;
        }
      },
      error: (error: any) => {
        console.log(error);
      }
    });
  }

  submit() {
    if (this.description != undefined) {
      this._todolist.addToDoList(this.description).subscribe({
        next: (result: boolean) => {
          this.successMessage = 'Task added successfully.';
          this.getAllList();
        },
        error: (error: any) => {
          console.log('Error occurred while adding the task:', error);
        }
      });
    } else {
      console.log('Please enter a valid task description.');
    }
  }

  startEditing(todo: IHome) {
    this.editId = todo.id;
    this.description = todo.description;
  }
  hideButton() {
    this.showButtons = false;
  }

  showButton() {
    this.showButtons = true;
  }

  updateToDoList() {
    if (this.editId !== undefined && this.description !== undefined) {
      const updatedTodo = this.home.find(todo => todo.id === this.editId);
      if (updatedTodo) {
        updatedTodo.description = this.description;
        this._todolist.editToDoList(updatedTodo.id, this.description).subscribe({
          next: (result: boolean) => {
            this.successMessage = 'Task updated successfully.';
            this.editId = undefined;
            this.description = undefined;
          },
          error: (error: any) => {
            console.log('Error occurred while updating the task:', error);
          }
        });
      }
    }
  }

  deleteToDoList(id: number) {
    this._todolist.DeleteToDoList(id).subscribe({
      next: () => {
        this.successMessage = 'Task deleted successfully.';
        this.getAllList();
      },
      error: (error: any) => {
        console.log('Error occurred while deleting the task:', error);
      }
    });
  }
}
