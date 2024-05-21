import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { AdminLayoutRoutes } from './admin-layout.routing';
import { UserComponent }            from '../../pages/user/user.component';

import { UpgradeComponent }         from '../../pages/upgrade/upgrade.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { IconsComponent } from 'app/pages/simulateur/icons.component';


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    NgbModule,
    ReactiveFormsModule
  ],
  declarations: [
    UserComponent,
    UpgradeComponent,
 
    IconsComponent,



  ]
})

export class AdminLayoutModule {}
