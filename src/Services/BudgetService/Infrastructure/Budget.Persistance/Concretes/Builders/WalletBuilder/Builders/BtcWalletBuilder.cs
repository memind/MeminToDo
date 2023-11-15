﻿using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.Abstractions.Builders.WalletBuilder.Builders
{
    public class BtcWalletBuilder : IWalletBuilder
    {
        private WalletDto _wallet;

        public void BuildCurrency() => _wallet.Currency = Domain.Enums.Currency.BTC;

        public void BuildTotal() => _wallet.Total = 0;

        public WalletDto GetWallet() => _wallet;

        public WalletDto BuildScheme(WalletDto model)
        {
            _wallet = model;

            BuildCurrency();
            BuildTotal();

            return GetWallet();
        }
    }
}
