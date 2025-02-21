import { atom } from "recoil";

export const userState = atom({
  key: "userState",
  default: {
    id: "",
    displayName: ""
  } as User,
});

export interface User {
    id?: string;
    displayName: string;
}