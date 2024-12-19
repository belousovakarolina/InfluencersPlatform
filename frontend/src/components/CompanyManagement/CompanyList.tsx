import React, { useState, useEffect } from 'react';
import {
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  CircularProgress,
  Alert,
} from '@mui/material';

import axios from '../../api/axiosConfig'; // Import configured Axios instance

// Define the Company interface based on the API response
interface Company {
  id: number;
  userId: string;
  name: string;
  description?: string | null;
  yearlyIncome?: number | null;
}

const CompanyList: React.FC = () => {
  const [companies, setCompanies] = useState<Company[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    // Fetch the company data from the API
    axios
      .get('/companyprofile/')
      .then((response) => {
        setCompanies(response.data);
        setLoading(false);
      })
      .catch((err) => {
        console.error('Error fetching company profiles:', err);
        setError('Failed to fetch company profiles.');
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <CircularProgress />;
  }

  if (error) {
    return <Alert severity="error">{error}</Alert>;
  }

  return (
    <TableContainer component={Paper}>
      <Typography variant="h6" gutterBottom>
        Company List
      </Typography>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Id</TableCell>
            <TableCell>User Id</TableCell>
            <TableCell>Name</TableCell>
            <TableCell>Description</TableCell>
            <TableCell>Yearly Income</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {companies.map((company) => (
            <TableRow key={company.id}>
              <TableCell>{company.id}</TableCell>
              <TableCell>{company.userId}</TableCell>
              <TableCell>{company.name}</TableCell>
              <TableCell>{company.description || 'N/A'}</TableCell>
              <TableCell>
                {company.yearlyIncome !== null
                  ? `$${company.yearlyIncome?.toLocaleString()}`
                  : 'N/A'}
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default CompanyList;
