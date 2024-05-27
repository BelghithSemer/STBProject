import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { LoginComponent } from './login/login.component';
import { ManageRequestsComponent } from './employee/manage-requests/manage-requests.component';
import { RegisterComponent } from './pages/register/register.component';
import { LandingComponent } from './pages/landing/landing.component';
import { StatComponent } from './pages/stat/stat.component';




export const AppRoutes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  }, 
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
        {
      path: '',
      loadChildren: () => import('./layouts/admin-layout/admin-layout.module').then(x => x.AdminLayoutModule)
  }]},
  {
    path: 'homepage',
    component : LandingComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'stats',
    component: StatComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: '**',
    redirectTo: 'homepage'
  },
  
  {
    path: 'emplo',
    component: ManageRequestsComponent,
    children: [
        {
            path: 'employee',
            loadChildren: () => import('./employee/employee.module').then(m => m.EmployeeModule)
        }
    ]
}
]
  

