import { Component, OnInit } from '@angular/core';
import { Routes } from '@angular/router';
import { users } from 'app/models/users';


export interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}

export const ROUTES: RouteInfo[] = [
    { path: '/dashboard',    title: 'Dashboard',         icon:'nc-bank',       class: '' },
    { path: '/simulateur',       title: 'Simulateur du crédit',             icon:'nc-chart-bar-32',    class: '' },
    { path: '/demande',         title: 'Demande du crédit',              icon:'nc-paper',      class: '' },
 
    { path: '/user',        title: 'Mon Profile',      icon:'nc-single-02',  class: '' },


    { path: '/upgrade',     title: 'Upgrade to PRO',    icon:'nc-spaceship',  class: 'active-pro' },
];
export const EmpRoutes: RouteInfo[] = [
    { path: '/request',    title: 'ListDemandes',         icon:'nc-bank',       class: '' },
    { path: '/stats',    title: 'Statistique',         icon:'nc-bank',       class: '' },
]
@Component({
    moduleId: module.id,
    selector: 'sidebar-cmp',
    templateUrl: 'sidebar.component.html',
})

export class SidebarComponent implements OnInit {
    public menuItems: any[];
    user:users;
    ngOnInit() {
        this.user = JSON.parse(sessionStorage.getItem('user'));
        if(this.user.role == 'client'){
            this.menuItems = ROUTES.filter(menuItem => menuItem);
        }else{
            this.menuItems = EmpRoutes.filter( menuItem => menuItem);
        }
        
    }
}
