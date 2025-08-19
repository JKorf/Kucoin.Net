using CryptoExchange.Net.Objects.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net
{
    internal static class KucoinErrors
    {
        public static ErrorMapping SpotErrors { get; } = new ErrorMapping([

            new ErrorInfo(ErrorType.Unauthorized, false, "KYC required", "200009"),
            new ErrorInfo(ErrorType.Unauthorized, false, "API key invalid", "400003"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Passphrase invalid", "400004"),
            new ErrorInfo(ErrorType.Unauthorized, false, "IP address not allowed", "400006"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Access denied", "400007"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Order placement forbidden", "400200"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Token now allowed based on region", "400500"),
            new ErrorInfo(ErrorType.Unauthorized, false, "User frozen", "411100"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Signature error", "400005"),

            new ErrorInfo(ErrorType.InvalidTimestamp, false, "Invalid timestamp", "400002"),

            new ErrorInfo(ErrorType.SystemError, true, "Internal server error", "500000"),
            new ErrorInfo(ErrorType.SystemError, true, "System busy", "230005"),

            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", "102411"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Time range not supported", "102425"),

            new ErrorInfo(ErrorType.InvalidPrice, false, "Price too low", "102429"),

            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity too large", "102434"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity too low", "102435"),

            new ErrorInfo(ErrorType.UnknownOrder, false, "Unknown order", "102436"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Unknown symbol", "900001"),

            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol does not allow new orders currently", "200001"),
            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol does not allow cancellation currently", "200002"),
            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol is not yet trading", "400600"),
            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol is not currently trading", "600203"),

            new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate clientOrderId", "102426"),

            new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many open orders", "102427", "200003"),

            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "102421", "200004"),

            new ErrorInfo(ErrorType.IncorrectState, false, "Order is being processed and can't be canceled", "102428"),

            ],
            [
                new ErrorEvaluator("400100", (code, msg) => {
                    if (string.IsNullOrEmpty(msg))
                        return new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", code);

                    if (msg!.Equals("Unsupported trading pair."))
                        return new ErrorInfo(ErrorType.UnknownSymbol, false, "Unknown symbol", code);

                    return new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", code);
                })
                ]);

        public static ErrorMapping FuturesErrors { get; } = new ErrorMapping([
            new ErrorInfo(ErrorType.Unauthorized, false, "Not allowed to place orders based on region", "40010"),
            new ErrorInfo(ErrorType.Unauthorized, false, "API key does not exist", "400003"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Passphrase invalid", "400004"),
            new ErrorInfo(ErrorType.Unauthorized, false, "IP address not allowed", "400006"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "400007"),
            new ErrorInfo(ErrorType.Unauthorized, false, "User frozen", "411100"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Signature invalid", "400005"),

            new ErrorInfo(ErrorType.SystemError, true, "Internal server error", "500000"),

            new ErrorInfo(ErrorType.InvalidTimestamp, false, "Invalid timestamp", "400002"),

            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", "100001", "300000", "400100"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage too high", "300016"),

            new ErrorInfo(ErrorType.InvalidPrice, false, "Price worse than liquidation price", "300007"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Price too high", "300011"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Price too low", "300012"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "100003", "200003"),

            new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "300018"),

            new ErrorInfo(ErrorType.NoPosition, false, "No open position to close", "300009"),

            new ErrorInfo(ErrorType.MaxPosition, false, "Max position limit exceeded", "126042"),

            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Order placement/cancellation suspended", "300002"),
            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol is in settlement and does not accept orders", "300015"),

            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "200005", "300003"),

            new ErrorInfo(ErrorType.IncorrectState, false, "Order not in cancelable state", "100004"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Position in liquidation", "300014"),

            new ErrorInfo(ErrorType.RiskError, false, "Risk limit exceeded", "300005"),

            new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many open orders", "300001"),
            new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many stop orders", "300004"),

            new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests, IP blocked for 30 seconds", "1015"),
            new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests, blocked for 10 seconds", "200002"),
            new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests", "429000"),
            ]);
    }
}
