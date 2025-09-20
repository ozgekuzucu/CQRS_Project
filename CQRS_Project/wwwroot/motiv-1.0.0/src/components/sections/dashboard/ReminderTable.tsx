import { Box, Button, Paper, Stack, Typography } from '@mui/material';
import { DataGrid, useGridApiRef } from '@mui/x-data-grid';
import IconifyIcon from 'components/base/IconifyIcon';
import SearchFilter from 'components/common/SearchFilter';
import CustomPagination from 'components/common/CustomPagination';
import { columns, rows } from 'data/dashboard/reminderTableData';

const ReminderTable = () => {
  const apiRef = useGridApiRef();

  return (
    <Paper
      sx={(theme) => ({
        p: theme.spacing(2, 2.5),
        width: 1,
      })}
    >
      <Stack
        direction={{ xs: 'column', sm: 'row' }}
        alignItems="center"
        justifyContent="space-between"
        spacing={2}
      >
        <Typography variant="h5" color="common.black">
          Reminder
        </Typography>

        <SearchFilter apiRef={apiRef} />

        <Button
          variant="contained"
          color="secondary"
          sx={(theme) => ({
            p: theme.spacing(0.625, 1.5),
            borderRadius: 1.5,
          })}
          startIcon={<IconifyIcon icon="heroicons-solid:plus" />}
        >
          <Typography variant="body2">Add New</Typography>
        </Button>
      </Stack>

      <Box
        sx={{
          height: 330,
          width: 1,
          mt: 1.75,
        }}
      >
        <DataGrid
          apiRef={apiRef}
          columns={columns}
          rows={rows}
          initialState={{
            pagination: {
              paginationModel: {
                pageSize: 5,
              },
            },
          }}
        />
      </Box>
      <CustomPagination apiRef={apiRef} />
    </Paper>
  );
};

export default ReminderTable;
