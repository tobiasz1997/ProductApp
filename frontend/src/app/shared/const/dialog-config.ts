import {DynamicDialogConfig} from 'primeng/dynamicdialog';

export const defaultDialogConfig = (title: string): DynamicDialogConfig => ({
    header: title,
    modal: true,
    width: '50vw',
    closable: true
})
