import { Guid } from "guid-typescript";

export interface User {
    id?: Guid;
    DisplayName: string;
}

export interface ActiveUser {
  user?: User;
}

export interface UserState {
    user: User;
  }