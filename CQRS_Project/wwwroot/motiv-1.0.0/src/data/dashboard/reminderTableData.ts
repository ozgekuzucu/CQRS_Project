import { GridColDef } from '@mui/x-data-grid';

interface IReminderData {
  id: number;
  description: string;
  due: string;
  overdue: string;
  notify: string;
  status: string;
}

export const columns: GridColDef<(typeof rows)[number]>[] = [
  {
    field: 'description',
    headerName: 'Description',
    flex: 1.5,
    minWidth: 200,
  },
  {
    field: 'due',
    headerName: 'Due',
    flex: 1,
    minWidth: 150,
    sortable: false,
  },
  {
    field: 'overdue',
    headerName: 'Overdue',
    flex: 1,
    minWidth: 150,
    sortable: false,
  },
  {
    field: 'notify',
    headerName: 'Notify',
    flex: 1,
    minWidth: 150,
  },
  {
    field: 'status',
    headerName: 'Status',
  },
];

export const rows: IReminderData[] = [
  {
    id: 1,
    description: 'Urgent Safety Recall',
    due: '06/04/2022',
    overdue: '08/04/2022',
    notify: 'David Demo',
    status: 'Completed',
  },
  {
    id: 2,
    description: 'Maintenance Checkup',
    due: '12/04/2022',
    overdue: '15/04/2022',
    notify: 'John Smith',
    status: 'In Progress',
  },
  {
    id: 3,
    description: 'Oil Change',
    due: '18/04/2022',
    overdue: '20/04/2022',
    notify: 'Emma Johnson',
    status: 'Pending',
  },
  {
    id: 4,
    description: 'Tire Replacement',
    due: '25/04/2022',
    overdue: '27/04/2022',
    notify: 'Sophia Brown',
    status: 'Pending',
  },
  {
    id: 5,
    description: 'Brake Inspection',
    due: '30/04/2022',
    overdue: '02/05/2022',
    notify: 'James Wilson',
    status: 'Scheduled',
  },
  {
    id: 6,
    description: 'Annual Health Checkup',
    due: '10/05/2022',
    overdue: '12/05/2022',
    notify: 'Emily Taylor',
    status: 'Scheduled',
  },
  {
    id: 7,
    description: 'Dentist Appointment',
    due: '15/05/2022',
    overdue: '18/05/2022',
    notify: 'Michael Brown',
    status: 'In Progress',
  },
  {
    id: 8,
    description: 'Home Maintenance',
    due: '20/05/2022',
    overdue: '22/05/2022',
    notify: 'Olivia Martinez',
    status: 'Pending',
  },
  {
    id: 9,
    description: 'Grocery Shopping',
    due: '25/05/2022',
    overdue: '27/05/2022',
    notify: 'Daniel Johnson',
    status: 'Completed',
  },
  {
    id: 10,
    description: 'Travel Planning',
    due: '30/05/2022',
    overdue: '01/06/2022',
    notify: 'Ava Wilson',
    status: 'Pending',
  },
  {
    id: 11,
    description: 'Financial Audit',
    due: '05/06/2022',
    overdue: '08/06/2022',
    notify: 'Noah Garcia',
    status: 'Scheduled',
  },
  {
    id: 12,
    description: 'Project Deadline',
    due: '10/06/2022',
    overdue: '12/06/2022',
    notify: 'Isabella Smith',
    status: 'In Progress',
  },
];
