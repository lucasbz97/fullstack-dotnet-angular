import { UpdateDependentRequest } from "./update-dependent-request";

export interface UpdateUserRequest {
    id: number;
    name: string;
    age: number;
    dependents: UpdateDependentRequest[];
}