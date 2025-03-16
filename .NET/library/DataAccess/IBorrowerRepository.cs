using OneBeyondApi.DTOs;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IBorrowerRepository
    {
        public List<BorrowerDto> GetBorrowers();

        public Guid AddBorrower(Borrower borrower);

        public IEnumerable<BorrowerWithLoansDto> GetBorrowersWithActiveLoans();
    }
}
