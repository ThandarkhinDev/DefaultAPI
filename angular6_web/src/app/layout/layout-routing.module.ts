import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';

const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
            { path: '', redirectTo: 'dashboard' },
            { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
            { path: 'apps.adminlevel', loadChildren: './adminlevel/adminlevel.module#AdminlevelModule' },
            { path: 'apps.admin', loadChildren: './admin/admin.module#AdminModule' },
            { path: 'changepassword', loadChildren: './change-password/change-password.module#ChangePasswordModule' },
            { path: 'fileupload', loadChildren: './fileupload/fileupload.module#FileuploadModule' },
            { path: 'apps.income', loadChildren: './income/income.module#IncomeModule' },
            { path: 'apps.expense', loadChildren: './expense/expense.module#ExpenseModule' },
            { path: 'apps.report', loadChildren: './report/report.module#ReportModule' },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LayoutRoutingModule {}
