import { Typography, Stack, Pagination } from '@mui/material';
import {
  gridPageSelector,
  gridPageCountSelector,
  useGridSelector,
  gridPageSizeSelector,
} from '@mui/x-data-grid';
import { GridApiCommunity } from '@mui/x-data-grid/internals';
import { MutableRefObject } from 'react';

const CustomPagination = ({ apiRef }: { apiRef: MutableRefObject<GridApiCommunity> }) => {
  const page = useGridSelector(apiRef, gridPageSelector);
  const pageCount = useGridSelector(apiRef, gridPageCountSelector);
  const pageSize = useGridSelector(apiRef, gridPageSizeSelector);
  const rowsCount = apiRef.current.getRowsCount();

  return (
    <Stack
      direction={{ xs: 'column', sm: 'row' }}
      alignItems="center"
      justifyContent="space-between"
      gap={{ xs: 1 }}
      sx={{ width: 1, overflow: 'auto', py: { xs: 1, sm: 0 } }}
    >
      <Typography variant="body2" color="grey.600">
        Showing {`${page * pageSize + 1}-${page * pageSize + pageSize} of ${rowsCount}`}
      </Typography>
      <Pagination
        color="secondary"
        size="small"
        count={pageCount}
        page={page + 1}
        onChange={(event, value) => {
          event.preventDefault();
          apiRef.current.setPage(value - 1);
        }}
      />
    </Stack>
  );
};

export default CustomPagination;
