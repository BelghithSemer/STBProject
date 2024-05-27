import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { UserComponent } from '../../pages/user/user.component';

import { simulateurComponent } from '../../pages/simulateur/simulateur.component';

import { UpgradeComponent } from '../../pages/upgrade/upgrade.component';
import { demandeCredit } from 'app/pages/demandeCredit/demandeCredit.component';
import { ManageRequestsComponent } from 'app/employee/manage-requests/manage-requests.component';
import { StatComponent } from 'app/pages/stat/stat.component';






export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'user',           component: UserComponent },
    { path: 'simulateur',     component: simulateurComponent },
    { path: 'demande',           component: demandeCredit},
    { path: 'request',      component: ManageRequestsComponent },
    { path: 'upgrade',        component: UpgradeComponent },
    { path: 'stats',        component: StatComponent },

];
