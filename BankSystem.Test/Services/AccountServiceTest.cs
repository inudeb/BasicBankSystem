using BackSystem.Application.Interfaces;
using BackSystem.Application.Interfaces.Repositories;
using BankSystem.Application.Services;
using BankSystem.Domain;
using Moq;
using NUnit.Framework;

namespace BankSystem.Test.Services
{
    [TestFixture]
    public class AccountServiceTest
    {
        private List<AccountEntity> data;

        [SetUp]
        public void Setup()
        {
            data = new List<AccountEntity>
            {
                new AccountEntity
                {
                    AccountId = 1001,
                    Balance = 200,
                    Transactions = new List<TransactionEntity> {
                        new TransactionEntity { AccountId = 1001, Movement=70, TransactionDate = new DateTime(2023,4,6)},
                        new TransactionEntity { AccountId = 1001, Movement=70, TransactionDate = new DateTime(2023,4,9)},
                        new TransactionEntity { AccountId = 1001, Movement=70, TransactionDate = new DateTime(2023,4,21)},
                        new TransactionEntity { AccountId = 1001, Movement=70, TransactionDate = new DateTime(2023,5,3)},
                        new TransactionEntity { AccountId = 1001, Movement=-80, TransactionDate = new DateTime(2023,5,8)},
                    },
                     User = new UserEntity{
                         Salt ="44545445sa54d55da4s5da",
                         UserId = 50005,
                         UserName= "testUser",
                         UserPassword = "44545445sa54d55da4s5da"
                     },
                     UserId = 50005
                }
            };
        }
        [Test]
        public async Task Account_Cannot_Have_LessThan_100() 
        {

            Mock<IAccountRepository> accountRepo = new Mock<IAccountRepository>();
            accountRepo.Setup(x => x.GetQueryable())
                .Returns(data.AsQueryable());
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            uow.Setup(c => c.SaveChanges())
                .Returns(1);
            uow.Setup(c => c.AccountRepository)
                .Returns(accountRepo.Object);
            AccountService service = new AccountService(uow.Object);

            //act
           var result = await  service.Withdraw(1001, 180);

            Assert.IsNotNull(result);
            Assert.IsTrue(!result.IsSuccess);
            StringAssert.AreEqualIgnoringCase(result.Message, "The account can't have less than $100.");
        }


        [Test]
        public async Task Account_Cannot_Deposit_MoreThan_10000()
        {

            Mock<IAccountRepository> accountRepo = new Mock<IAccountRepository>();
            accountRepo.Setup(x => x.GetQueryable())
                .Returns(data.AsQueryable());
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            uow.Setup(c => c.SaveChanges())
                .Returns(1);
            uow.Setup(c => c.AccountRepository)
                .Returns(accountRepo.Object);
            AccountService service = new AccountService(uow.Object);

            //act
            var result = await service.Deposit(1001, 11000);

            Assert.IsNotNull(result);
            Assert.IsTrue(!result.IsSuccess);
            StringAssert.AreEqualIgnoringCase(result.Message, "Cannot deposit more than $10,000 in a single transaction.");
        }

        [Test]
        public async Task Account_Cannot_Withdraw_MoreThan_90Percent()
        {
            data = new List<AccountEntity>
            {
                new AccountEntity
                {
                    AccountId = 1001,
                    Balance = 2000,
                    Transactions = new List<TransactionEntity> {
                        new TransactionEntity { AccountId = 1001, Movement=700, TransactionDate = new DateTime(2023,4,6)},
                        new TransactionEntity { AccountId = 1001, Movement=700, TransactionDate = new DateTime(2023,4,9)},
                        new TransactionEntity { AccountId = 1001, Movement=700, TransactionDate = new DateTime(2023,4,21)},
                        new TransactionEntity { AccountId = 1001, Movement=700, TransactionDate = new DateTime(2023,5,3)},
                        new TransactionEntity { AccountId = 1001, Movement=-800, TransactionDate = new DateTime(2023,5,8)},
                    },
                     User = new UserEntity{
                         Salt ="44545445sa54d55da4s5da",
                         UserId = 50005,
                         UserName= "testUser",
                         UserPassword = "44545445sa54d55da4s5da"
                     },
                     UserId = 50005
                }
            };

            Mock<IAccountRepository> accountRepo = new Mock<IAccountRepository>();
            accountRepo.Setup(x => x.GetQueryable())
                .Returns(data.AsQueryable());
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            uow.Setup(c => c.SaveChanges())
                .Returns(1);
            uow.Setup(c => c.AccountRepository)
                .Returns(accountRepo.Object);
            AccountService service = new AccountService(uow.Object);

            //act
            var result = await service.Withdraw(1001, 1850);

            Assert.IsNotNull(result);
            Assert.IsTrue(!result.IsSuccess);
            StringAssert.AreEqualIgnoringCase(result.Message, "Cannot withdraw more than 90% of the total balance on a single transacction.");
        }
    }
}
