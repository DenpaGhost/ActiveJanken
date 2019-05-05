using System;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace Models
{
    public class Player
    {
        private Sukumi _pickupSukumi;

        public Sukumi PickupSukumi
        {
            get => _pickupSukumi;
            set
            {
                _pickupSukumi = value;
                _stopwatch.Restart();
            }
        }

        public bool IsEnablePickupSukumi => _stopwatch.ElapsedMilliseconds <= Constants.EnablePickupSukumiTime;
        
        private readonly Text _text;
        public string Message
        {
            get => _text.text;
            set => _text.text = value;
        }

        private PlayerState _state = PlayerState.Idle;

        public PlayerState State
        {
            get => _state;
            set
            {
                _state = value;
                switch (_state)
                {
                    case PlayerState.Idle:
                        OnIdle();
                        break;
                    case PlayerState.Ready:
                        OnReady();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public readonly Image PickupSukumiImage;
        public readonly ResultText ResultText;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        public Player(RpsButton rock, RpsButton scissors, RpsButton paper, Image pickupSukumiImage, Text text,
            ResultText resultText)
        {
            rock.player = this;
            rock.sukumi = Sukumi.rock;

            scissors.player = this;
            scissors.sukumi = Sukumi.scissors;

            paper.player = this;
            paper.sukumi = Sukumi.paper;

            PickupSukumiImage = pickupSukumiImage;
            _text = text;
            ResultText = resultText;
        }

        private void OnIdle()
        {
            Message = Constants.WaitingMessage;
            _stopwatch.Stop();
        }

        private void OnReady()
        {
            Message = Constants.ReadyMessage;
            _stopwatch.Start();
        }

        [CanBeNull]
        public static Player Battle(Player playerA, Player playerB)
        {
            if (!(playerA.IsEnablePickupSukumi && playerB.IsEnablePickupSukumi))
            {
                return null;
            }

            switch (playerA.PickupSukumi)
            {
                case Sukumi.rock:
                    switch (playerB.PickupSukumi)
                    {
                        case Sukumi.rock:
                            return null;
                        case Sukumi.scissors:
                            return playerA;
                        case Sukumi.paper:
                            return playerB;
                        default:
                            return null;
                    }

                case Sukumi.scissors:
                    switch (playerB.PickupSukumi)
                    {
                        case Sukumi.rock:
                            return playerB;
                        case Sukumi.scissors:
                            return null;
                        case Sukumi.paper:
                            return playerA;
                        default:
                            return null;
                    }

                case Sukumi.paper:
                    switch (playerB.PickupSukumi)
                    {
                        case Sukumi.rock:
                            return playerA;
                        case Sukumi.scissors:
                            return playerB;
                        case Sukumi.paper:
                            return null;
                        default:
                            return null;
                    }

                default:
                    return null;
            }
        }
    }
}