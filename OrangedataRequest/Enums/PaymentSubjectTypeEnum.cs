namespace OrangedataRequest.Enums
{
    /// <summary>
    ///     Предмет расчета
    /// </summary>
    public enum PaymentSubjectTypeEnum
    {
        /// <summary>
        ///     Товар
        /// </summary>
        Product = 1,
        /// <summary>
        ///     Подакцизный товар
        /// </summary>
        Excisable,
        /// <summary>
        ///     Работа
        /// </summary>
        Job,
        /// <summary>
        ///     Услуга
        /// </summary>
        Service,
        /// <summary>
        ///     Ставка азартной игры
        /// </summary>
        GamblingBet,
        /// <summary>
        ///     Выигрыш азартной игры
        /// </summary>
        GamblingGain,
        /// <summary>
        ///     Лотерейный билет
        /// </summary>
        LotteryTicket,
        /// <summary>
        ///     Выигрыш лотереи
        /// </summary>
        LotteryWinnings,
        /// <summary>
        ///     Предоставление РИД
        /// </summary>
        RID,
        /// <summary>
        ///     Платёж
        /// </summary>
        Payment,
        /// <summary>
        ///     Агентское вознаграждение
        /// </summary>
        AgentComission,
        /// <summary>
        ///     Составной предмет расчета
        /// </summary>
        Composite,
        /// <summary>
        ///     Иной предмет расчета
        /// </summary>
        Other,
        /// <summary>
        ///     Имущественное право
        /// </summary>
        PropertyRight,
        /// <summary>
        ///     Внереализационный доход
        /// </summary>
        NonOperatingIncome,
        /// <summary>
        ///     Страховые взносы
        /// </summary>
        InsurancePremiums,
        /// <summary>
        ///     Торговый сбор
        /// </summary>
        TradingFee,
        /// <summary>
        ///     Курортный сбор
        /// </summary>
        ResortFee,
        /// <summary>
        ///     Залог
        /// </summary>
        Pledge,
        /// <summary>
        /// Расход
        /// </summary>
        Consumption,
        /// <summary>
        /// Взносы на обязательное пенсионное страхование ИП
        /// </summary>
        ContributionsPensionForIndividualEntrepreneurs,
        /// <summary>
        /// Взносы на обязательное пенсионное страхование
        /// </summary>
        ContributionsPension,
        /// <summary>
        /// Взносы на обязательное медицинское страхование ИП
        /// </summary>
        ContributionsHealthForIndividualEntrepreneurs,
        /// <summary>
        /// Взносы на обязательное медицинское страхование
        /// </summary>
        ContributionsHealth,
        /// <summary>
        /// Взносы на обязательное социальное страхование
        /// </summary>
        ContributionsSocial,
        /// <summary>
        /// Платеж казино
        /// </summary>
        CasinoPayment,
        /// <summary>
        /// Выдача денежных средств
        /// </summary>
        IssuanceOfFunds,
        /// <summary>
        /// АТНМ (не имеющем кода маркировки)
        /// </summary>
        ATNM = 30,
        /// <summary>
        /// АТМ (имеющем код маркировки)
        /// </summary>
        ATM,
        /// <summary>
        /// ТНМ
        /// </summary>
        TNM,
        /// <summary>
        /// ТМ
        /// </summary>
        TM
    }
}
