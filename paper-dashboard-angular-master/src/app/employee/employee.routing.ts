import { Routes } from "@angular/router";
import { ManageRequestsComponent } from "./manage-requests/manage-requests.component";

export const EmployeeRoutes: Routes = [
    { path: 'request',      component: ManageRequestsComponent },
]